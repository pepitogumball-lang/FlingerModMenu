import time
import subprocess
import sys
import os
from watchdog.observers import Observer
from watchdog.events import FileSystemEventHandler

DEBOUNCE_SECONDS = 5

IGNORE_PATTERNS = {
    ".git",
    "__pycache__",
    ".pythonlibs",
    "bin",
    "obj",
    ".local",
}

IGNORE_EXTENSIONS = {
    ".pyc",
    ".pyo",
    ".swp",
    ".tmp",
}


def should_ignore(path):
    parts = path.replace("\\", "/").split("/")
    for part in parts:
        if part in IGNORE_PATTERNS:
            return True
    _, ext = os.path.splitext(path)
    if ext in IGNORE_EXTENSIONS:
        return True
    return False


class ChangeHandler(FileSystemEventHandler):
    def __init__(self):
        self._last_trigger = 0
        self._pending = False

    def on_any_event(self, event):
        if event.is_directory:
            return
        src = event.src_path
        if should_ignore(src):
            return
        now = time.time()
        if now - self._last_trigger < DEBOUNCE_SECONDS:
            return
        self._last_trigger = now
        print(f"[watcher] Change detected: {src}", flush=True)
        self._run_push()

    def _run_push(self):
        print("[watcher] Running push.py ...", flush=True)
        result = subprocess.run(
            [sys.executable, "push.py"],
            capture_output=False,
        )
        if result.returncode == 0:
            print("[watcher] push.py completed successfully.", flush=True)
        else:
            print(f"[watcher] push.py exited with code {result.returncode}.", flush=True)


def main():
    watch_dir = os.path.dirname(os.path.abspath(__file__))
    print(f"[watcher] Watching for changes in: {watch_dir}", flush=True)
    print(f"[watcher] Debounce interval: {DEBOUNCE_SECONDS}s", flush=True)
    print("[watcher] Press Ctrl+C to stop.", flush=True)

    handler = ChangeHandler()
    observer = Observer()
    observer.schedule(handler, path=watch_dir, recursive=True)
    observer.start()

    try:
        while True:
            time.sleep(1)
    except KeyboardInterrupt:
        print("[watcher] Stopping...", flush=True)
        observer.stop()
    observer.join()


if __name__ == "__main__":
    main()
