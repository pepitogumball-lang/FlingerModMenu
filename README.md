# FlingerModMenu

Mod Menu táctil para **Hollow Knight (Android)** — desarrollado en C# con Unity IMGUI y la Modding API de Hollow Knight.

## Características

- Botón flotante arrastrable ("smiler") en pantalla
- **God Mode** — inmunidad total al daño
- **Infinite Jump** — salto ilimitado
- **One Hit Kill** — mata enemigos de un golpe (9999 daño)
- **Desbloquear Movimientos** — dash, walljump, double jump, superdash, etc.
- **Amuletos Infinitos** — 99 slots de amuleto
- **Give Geo** — añade la cantidad de geo que quieras

## Compilar

### 1. Sube las dependencias a `lib/`

Copia desde `Android/data/[tu_juego]/files/hollow_knight_Data/Managed/` los siguientes archivos:

- `Assembly-CSharp.dll`
- `ModdingAPI.dll`
- `UnityEngine.dll`
- `UnityEngine.CoreModule.dll`
- `UnityEngine.IMGUIModule.dll`
- `PlayMaker.dll`

### 2. Presiona Run en Replit

Se ejecutará automáticamente:
```
xbuild FlingerModMenu.csproj /p:Configuration=Release
```

El archivo compilado estará en: `bin/Release/FlingerModMenu.dll`

### 3. Instala en Android

Lee el archivo `instrucciones_android.txt` para los pasos detallados con ZArchiver y Shizuku.
