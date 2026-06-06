# Welding Simulation Project

A 3D interactive welding simulator built in **Unity 6** that demonstrates real-time user input handling, UI integration, physics normalization, and dynamic material rendering.

## 🚀 Features
* **Voltmeter Gatekeeper:** Restricts simulation access via a starting Lock Panel until a valid input voltage between **50V and 90V** is submitted.
* **Frame-Rate Independent Physics:** Simulates metal heating and cooling cooling models consistently across different hardware configurations using `Time.deltaTime`.
* **Dynamic Visual Effects:** Includes an adaptive particle spark emitter, a specialized trail renderer, and real-time randomized light flickering to simulate a violent electric arc flash.
* **Emission Shader Control:** Dynamically drives the color properties (`_EmissionColor`) of the filler material based on real-time calculated heat variables.

---

## 🛠️ Project Structure & Architecture

The project relies on clean separation of concerns, split across four core scripts:

### 1. `TorchFollow.cs` (The Simulation Brain)
Maps 2D mouse position into 3D world space coordinates to guide the torch. It normalizes the user's high-voltage input (50-90V) down to a stable math scale to safely drive light intensity, spark emission density, and metal heating values frame-by-frame. It also filters mouse clicks using `EventSystem.current.IsPointerOverGameObject()` so clicking the UI doesn't fire the torch.

### 2. `GameManager.cs` (Scene State Coordinator)
Handles structural setup and resetting the simulation environment. It directly accesses the filler metal's `MeshRenderer` to wipe out active weld beads by dropping the material's Alpha channel back down to `0f` (completely transparent).

### 3. `LightFlicker.cs` (Cosmetic Polish)
An independent rendering script that samples the torch's light component frame-by-frame, continuously throwing randomized mathematical noise (`Random.Range(40f, 100f)`) onto the light intensity value to replicate a realistic electric welding arc.

### 4. `MainMenuController.cs` (System Controller)
Triggers `Application.Quit()` to cleanly shut down execution loops on standard full-screen desktop application builds, utilizing preprocessor flags (`#if UNITY_EDITOR`) to gracefully close play mode during development testing.

---

## ⚙️ Technical Highlights For Reviewers
* **Normalization:** Rather than multiplying values directly by 90V (which would break light systems and trigger frame rate drops from spawning thousands of sparks), the scripts scale input values safely down to relative fractions.
* **Input Blocking:** Prevents accidental gameplay raycasts through the UI by reading the canvas interaction layers.

## 💻 Built With
* **Engine:** Unity 6 (6000.3.10f1)
* **Scripting Language:** C# 
* **IDE:** Microsoft Visual Studio / VS Code
