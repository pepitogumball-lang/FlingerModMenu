# Dependencias de lib/

## Ya incluidas (extraídas del release oficial v77)

| Archivo | Descripción |
|---|---|
| `Assembly-CSharp.dll` | Juego parcheado — contiene namespace `Modding` |
| `MMHOOK_Assembly-CSharp.dll` | Hooks MonoMod (equivalente moderno de ModdingAPI.dll) |
| `MMHOOK_PlayMaker.dll` | Hooks de PlayMaker |
| `MonoMod.RuntimeDetour.dll` | Dependencia de MonoMod |
| `MonoMod.Utils.dll` | Dependencia de MonoMod |
| `Mono.Cecil.dll` | Dependencia de MonoMod |
| `Newtonsoft.Json.dll` | JSON serialization |

## Faltan — debes subirlas desde tu instalación del juego

Cópialas desde:
`hollow_knight_Data/Managed/` (PC Steam) o dentro de la APK Mono

| Archivo | Origen |
|---|---|
| `UnityEngine.dll` | Carpeta `Managed/` del juego |
| `UnityEngine.CoreModule.dll` | Carpeta `Managed/` del juego |
| `UnityEngine.IMGUIModule.dll` | Carpeta `Managed/` del juego |
| `PlayMaker.dll` | Carpeta `Managed/` del juego |
