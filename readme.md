# RSS

![RSS:shield-version]
![RSS:shield-license]

**Real Solar System (RSS)** is an add-on for **Kerbal Space Program (KSP)** that converts the Kerbol System into the (Real) Solar System.

## Dependencies

1. **BurstPQS** (by **Phantomical**). See the **[GitHub repository][RSS:BurstPQS]** for detail, license and source.
2. **CustomBarnKit** (by **sarbian**). See the **[GitHub repository][RSS:CBK]** for details, license and source.
3. **Ferram Aerospace Research** (by **ferram4** and **dkavolis**.) See the **[GitHub repository][RSS:FAR]** for details, license and source.
4. **KSCSwitcher** (by **regex**, **NathanKell** and **jbengtson**). See the **[GitHub repository][RSS:KSCSwitcher]** for details, license and source.
5. **Kopernicus** (by **teknoman**, **bryce**, **Thomas P.** and **NathanKell**). See the **[GitHub repository][RSS:Kopernicus]** for details, license and source.
6. **Module Manager** (by **sarbian**, **swamp_ig** and **ialdabaoth**). See the **[GitHub repository][RSS:ModuleManager]** for details, license and source.

## Installation

Starting with a **clean** KSP installation:

1. Download the latest version of **Kopernicus**.
2. Download the latest version of **BurstPQS**.
3. Download the latest version of **KSCSwitcher**.
4. Download the latest version of **CustomBarnKit**.
5. Install the mods according to the instructions provided by each one.
6. Download the latest version of **RealSolarSystem**. Both **"RealSolarSystem-Core"** and **"RealSolarSystem-Textures"** are required.
7. Optionally download the **"RealSolarSystem-Visuals"** to add clouds and other atmosphere effects. This pack requires **EnvironmentalVisualEnhancements** and **Scatterer**!
8. Although it is not a hard dependency it is highly recommended to install **TiltEm** for proper axial tilt.
9. Extract the contents of the **"RealSolarSystem-Core"**, **"RealSolarSystem-Textures"** and, optionally, the **"RealSolarSystem-Visuals"** .zip files.
10. Merge the resulting **GameData** folders with the existing KSP GameData folder. The final directory structure should look like this:

```
GameData
└───┬──── ModuleManager.dll
    ├──── 000_Harmony
    ├──── BurstPQS
    ├──── CustomBarnKit
    ├──── (EnvironmentalVisualEnhancements)
    ├──── FerramAerospaceResearch
    ├──── Kopernicus
    ├──── KSCSwitcher
    ├──── KSPTextureLoader
    ├──── ModularFlightIntegrator
    ├──── RealSolarSystem
    ├──── RSS-Textures
    ├──── (RSSVE)
    ├──── (Scatterer)
    ├──── (StockScattererConfigs)
    └──── Squad
```

10. Launch KSP and enjoy the new solar system!

## Suggested Mods

- **BetterTimeWarp** (by **MrHappyFace** and **LinuxGuruGamer**). Enables custom time warp rates. See the **[KSP forum thread][RSS:BetterTimeWarp]** for details, license and source.
- **Custom Asteroids** (by **Starstrider42**). Better asteroid generation and new types. See the **[KSP forum thread][RSS:CustomAsteroids]** for details, license and source.
- **KerbalWind** (by **DaMichel** and **RCrockford**). Your ships will now have to contend with various wind strengths and directions. See the **[KSP forum thread][RSS:KerbalWind]** for details, license and source.
- **Not In My BackYard** (by **magico13** and **LinuxGuruGamer**). Ship recovery can now only be done in pre-defined sites. See the **[KSP forum thread][RSS:NIMBY]** for details, license and source.
- **Principia** (by **eggrobin** and **pleroy**). Adds N-body simulation to all solar system bodies and ships. See the **[GitHub repository][RSS:Principia]** for details, license and source.
- **Rational Resources** (by **JadeOfMaar**). Improves resource distribution for all solar system bodies. See the **[KSP forum thread][RSS:RationalResources]** for details, license and source.
- **RealAntennas** (by **DRVeyl** and the **KSP-RO Team**). Communications now depend on the physical parameters of the antenna and not on pre-defined distance values. See the **[GitHub repository][RSS:RealAntennas]** for details, license and source.
- **SCANsat** (by **DMagic** and the **KSPModStewards Team**). Creates maps of the solar system bodies surfaces. See the **[GitHub repository][RSS:SCANSAT]** for details, license and source.
- **TextureReplacer** (by **shaw**). Allows replacement of many in-game textures. See the **[KSP forum thread][RSS:TextureReplacer]** for details, license and source.
- **TiltEm** (by **Dagger** and **BallisticFox**). Adds proper axial tilt for all solar system bodies. See the **[GitHub repository][RSS:TiltEm]** for details, license and source.

## Credits

### Textures:

- Mercury by **BallisticFox** (redistributed with permission)
- Ceres and Vesta by **USGS**
- Jupiter by the **Planetary Society**
- Saturn moons by **CICLOPS**
- Pluto and Charon by **supersean08**
- Eris by **Solar System Scope**
- Special thanks to **Björn Jónsson** and **John van Vliet** (Celestia Motherlode) for their work on many other bodies.

### Programming:

- **Real Solar System (RSS)** by **[NathanKell][RSS:NathanKell]** and **[KSP-RO contributors][RSS:KSP-RO-RSS]**

## License

RealSolarSystem-Core & RealSolarSystem-Textures are licensed under a **Creative Commons Attribution-NonCommercial-ShareAlike 4.0 (CC BY-NC-SA 4.0)** license.

You should have received a copy of the license along with this work. If not, visit the **[official Creative Commons web page][RSS:license]**.

Note that the above license does not cover mod packs. Redistributing this work via a mod pack is not allowed.

RealSolarSystem-Visuals is licensed under an **All Rights Reserved (ARR)** license. You may not redistribute or re-use these assets without express permission from the author.

***

[RSS:BetterTimeWarp]:         https://forum.kerbalspaceprogram.com/index.php?showtopic=154935
[RSS:BurstPQS]:               https://github.com/Phantomical/BurstPQS
[RSS:CBK]:                    https://github.com/sarbian/CustomBarnKit
[RSS:CICLOPS]:                http://www.ciclops.org
[RSS:CustomAsteroids]:        https://forum.kerbalspaceprogram.com/index.php?showtopic=72785
[RSS:FAR]:                    https://github.com/KSPModStewards/Ferram-Aerospace-Research
[RSS:KSCSwitcher]:            https://github.com/KSP-RO/KSCSwitcher
[RSS:KerbalWind]:             https://forum.kerbalspaceprogram.com/index.php?showtopic=195587
[RSS:Kopernicus]:             https://github.com/Kopernicus/Kopernicus
[RSS:KSP-RO-RSS]:             https://github.com/KSP-RO/RealSolarSystem
[RSS:license]:                https://creativecommons.org/licenses/by-nc-sa/4.0
[RSS:ModuleManager]:          https://github.com/sarbian/ModuleManager
[RSS:NathanKell]:             https://github.com/NathanKell
[RSS:NIMBY]:                  https://forum.kerbalspaceprogram.com/index.php?showtopic=178484
[RSS:Principia]:              https://github.com/mockingbirdnest/Principia
[RSS:RationalResources]:      https://forum.kerbalspaceprogram.com/index.php?showtopic=184875
[RSS:RealAntennas]:           https://github.com/KSP-RO/RealAntennas
[RSS:SCANSAT]:                https://github.com/KSPModStewards/SCANsat
[RSS:shield-license]:         https://img.shields.io/badge/License-CC--BY--NC--SA-red
[RSS:shield-version]:         https://img.shields.io/badge/KSP%20Version-1.12.5-green
[RSS:TextureReplacer]:        https://forum.kerbalspaceprogram.com/index.php?showtopic=96851
[RSS:TiltEm]:                 https://github.com/ballisticfox/TiltEm-Continued
