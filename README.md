# Wood-Processing Machine XR Training System

**OSU CS 461 Capstone — CS.062 | Fall 2026**  
**Engine:** Unity 6000.6.5f1 (Unity 6.6), C#

This project is developing an **XR training application** for learning to operate wood-processing machinery, initially a woodworking press. The official project objectives cover an interactive 3D machine and workspace; guided startup, operating, and shutdown or safe-state procedures; safety checks and warnings; detection of unsafe or incorrect actions; instructional feedback; task, error, and progress tracking; data logging; and a reusable training architecture with technical and user documentation.

> **Current status:** The reported implementation is an **object-interaction prototype in the Unity Editor**, not yet a complete AR training application. Current functionality and file locations should be checked against the commit being submitted for the Week 5 checkpoint.

## Current prototype

The current scene is reported to support:

- Selecting objects with a raycast through `InputMaster`.
- Translating selected objects along X, Y, and Z using TextMeshPro input fields (`TranslationInteraction`).
- Rotating selected objects using TextMeshPro input fields (`RotationInteraction`).
- Changing a selected object's RGB color using sliders (`ColorChanger`).

A PlayMode integration test, `RaycastHit_SelectsCorrectObject`, checks the actual raycast selection behavior. One GameCI test report showed **1/1 tests passed**. See [Testing](#testing) for the distinction between a passing test and a successful workflow run.

The machine-specific 3D model, AR placement, training procedures, safety validation, progress tracking, and data logging are **planned**, not claimed as implemented.

## Requirements and development tools

### To open and run the current Editor prototype

- [Git](https://git-scm.com/) and access to the [project repository](https://github.com/2026-2027-Capstone-Projects/CS.062-XR-Training-for-Wood-Machinery-Operation).
- [Unity Hub](https://unity.com/download) and **Unity Editor 6000.6.5f1**. Confirm the version in `Unity Project Files/Demo Interactive Application/ProjectSettings/ProjectVersion.txt`.
- The Unity packages recorded in the project's `Packages/manifest.json` and `Packages/packages-lock.json`; allow Unity to resolve them when the project opens.

### For planned Android and iOS AR builds

The team selected **Unity Build Profiles and Unity's built-in build pipeline**. The planned development setup includes these Unity Hub modules:

- **Android Build Support**, including **Android SDK & NDK Tools** and **OpenJDK**.
- **iOS Build Support**.
- **macOS Build Support** for macOS desktop builds. This is distinct from iOS Build Support and does not replace Xcode.

The standard Unity AR/XR packages selected by the team are:

| Unity package | Package identifier | Role |
| --- | --- | --- |
| AR Foundation | `com.unity.xr.arfoundation` | Cross-platform AR features |
| Google ARCore XR Plug-in | `com.unity.xr.arcore` | Android AR provider |
| Apple ARKit XR Plug-in | `com.unity.xr.arkit` | iOS AR provider |
| XR Plug-in Management | `com.unity.xr.management` | Platform XR configuration |
| XR Interaction Toolkit | `com.unity.xr.interaction.toolkit` | XR/AR interaction tools |

Use package versions compatible with Unity **6000.6.5f1** and keep their versions tracked in the Unity package manifest and lockfile. **[VERIFY which packages are currently installed and which providers are enabled.]** The XR Interaction Toolkit was reported as already installed, but its exact version has not been confirmed.

Local iOS device builds additionally require **macOS, Xcode, an Apple signing setup, and authorized developer account access**. Mobile development and distribution also require compatible AR-capable Android/iOS devices.

**Architecture decision:** Use Unity's AR Foundation/ARCore/ARKit/XR Plug-in Management/XR Interaction Toolkit stack rather than creating a custom XR tracking or interaction framework. Project-specific development focuses on the machine and training behavior.

## Quickstart: from a clean clone to a running scene

1. Clone the repository:

   ```bash
   git clone https://github.com/2026-2027-Capstone-Projects/CS.062-XR-Training-for-Wood-Machinery-Operation.git
   cd CS.062-XR-Training-for-Wood-Machinery-Operation
   ```

2. In Unity Hub, choose **Add project from disk** and select **`Unity Project Files/Demo Interactive Application/`**. This is the folder containing `Assets`, `Packages`, and `ProjectSettings`.
3. Open the project using **Unity 6000.6.5f1**. Let Unity complete its initial package resolution and asset import.
4. In the Project window, open **`Assets/Scenes/SampleScene.unity`** **[VERIFY the scene is present at the named checkpoint commit]**.
5. Press **Play**.
6. **Expected prototype behavior:** left-click the boot or monkey-head object to select it; enter X/Y/Z values to change its position or rotation; use the RGB sliders to change the selected object's color.
7. Stop Play mode when finished.

**Checkpoint requirement:** Have a teammate who did not initially set up the project perform these steps on a fresh clone. Fix any undocumented requirements before generating the checkpoint PDF.

## Testing

### Run the PlayMode test locally

1. Open the Unity project in Unity **6000.6.5f1**.
2. Open **Window → General → Test Runner**.
3. Select **PlayMode** and run `RaycastHit_SelectsCorrectObject` (or **Run All**).
4. Verify the result is **Passed**. The test creates a cube with a `BoxCollider` and `Interactable`, simulates a click on the Input System's `Attack` action, and asserts that the existing `InputMaster.SelectedObject` references the cube. It uses screen coordinate `(0, 0)` to align with the observed mouse position during the test. No test-only helper was added to `InputMaster`.

### GitHub Actions

The repository uses **two independent workflows**, each with its own checkout:

| Workflow | File | What it does |
| --- | --- | --- |
| **CI Build** | `.github/workflows/ci.yml` | `game-ci/unity-builder@v4` builds for `StandaloneLinux64` |
| **Unity PlayMode Tests** | `.github/workflows/unity-tests.yml` | `game-ci/unity-test-runner@v4` executes PlayMode tests and uploads results |

Both workflows are intended to run for pull requests targeting `main`, pushes to `main`, and manual dispatch. Keeping them separate avoids the earlier issue in which a test runner changed package files and generated artifacts before the builder's clean-tree version check.

**Recorded result:** GameCI parsed a PlayMode XML report with **1/1 tests passed in 0.113 seconds**. A previous combined workflow subsequently failed when publishing a GitHub check, so the passing XML result alone does not establish that the final separate **Unity PlayMode Tests** workflow has passed.

**Deliberate failure demonstration:** The team reported a temporary run with the expected-selection assertion changed to `Is.SameAs(null)`: [test demonstration run](https://github.com/2026-2027-Capstone-Projects/CS.062-XR-Training-for-Wood-Machinery-Operation/actions/runs/41378977144). This shows that an incorrect assertion can make a test fail, **not** by itself that the test detects a regression in production behavior. For the checkpoint's stronger requirement, temporarily break `InputMaster`'s selection assignment on a disposable branch, confirm that the *unchanged* test fails in CI, then restore the implementation and confirm a passing run. Do not merge the deliberately broken behavior.

### Local builds

Use **File → Build Profiles** in Unity to configure an appropriate standalone or mobile player build. The existing CI build uses `StandaloneLinux64`

## Week 10 delivery plan — provisional project-manager decision

**No delivery platform has been confirmed by the Product Owner.** On **2026-11-01**, **Owen Koster (project manager)** recorded the following **working decision** so the team has a concrete plan for the Week 10 phone/tablet AR walking skeleton. This is Owen's current best estimate of a workable delivery route, **not a partner-approved requirement, a full-team sign-off, or evidence that accounts/releases exist**. It remains an interim plan, not the full XR project scope.

| Platform | Distribution | Week 10 evidence target |
| --- | --- | --- |
| Android | **Google Play internal testing** | Signed `.aab` delivered through Play Console, a working tester opt-in/install link, and a successful installation on an eligible device outside the student team |
| iOS | **Apple TestFlight external beta** | Signed iOS build uploaded using macOS/Xcode and App Store Connect, beta access after any applicable review, and a successful installation on an eligible outside-team device |

The planned walking skeleton is a **small end-to-end AR experience** with a 3D machine representation, meaningful interaction, and an initial guided training fragment with feedback. The exact slice must be confirmed with the Product Owner. **Decision rationale:** an installable store-managed test build would let outside-team users access the prototype without transferring a Unity project or manually sideloading an app. **Decision status:** provisional; it may change if the Product Owner asks for another delivery approach or if accounts, signing, device access, or beta review make this plan impractical.

**Outstanding prerequisites:** Product Owner/mentor agreement to this proposed Week 10 delivery plan; Google Play and Apple developer account access and owners; signing arrangements; macOS/Xcode access; compatible devices; testers; beta review; and actual release URLs. **Owen Koster owns the provisional decision and coordinates partner confirmation, scheduling, and checkpoint evidence as project manager; technical build and publishing owners must be assigned separately.** See [`docs/design.md`](docs/design.md) for the deployment plan and [`docs/risks.md`](docs/risks.md) for ranked risks and external dependencies.

If the Product Owner/mentor does not agree to these channels, or platform access cannot be arranged, **record a revised distribution decision and communicate the change before claiming the Week 10 production target is settled**. When channels are active, add actual opt-in/invitation links, supported devices, installation instructions, smoke-test results, and rollback procedure. **Do not describe the distributions as live until verified.**

## Documentation and workflow

- [`docs/requirements.md`](docs/requirements.md) — official project objectives, scope, acceptance criteria, constraints, and open questions.
- [`docs/design.md`](docs/design.md) — existing and proposed architecture, interfaces, and deployment approach.
- [`docs/risks.md`](docs/risks.md) — the **single** ranked project risk register.
- [`CONTRIBUTING.md`](CONTRIBUTING.md) — setup, CI, review process, and Definition of Done.
- [`AGENTS.md`](AGENTS.md) — navigation and instructions for AI-enabled contributors.

The Product Owner joins weekly in-person reviews and coordinates through Microsoft Teams. Source materials—including manufacturer manuals, SOPs, and live notes—are shared via Teams and **must not be published or uploaded to external tools without permission**. Verify safety-critical training procedures against the authoritative source and an appropriate human reviewer.

This is a training prototype, **not a physical machine controller or a substitute for supervised operator instruction**. **[VERIFY that this scope boundary is agreed with the Product Owner.]**

## Contacts and links

- **Repository:** [CS.062-XR-Training-for-Wood-Machinery-Operation](https://github.com/2026-2027-Capstone-Projects/CS.062-XR-Training-for-Wood-Machinery-Operation)
- **Sprint board:** [GitHub Projects — current view](https://github.com/orgs/2026-2027-Capstone-Projects/projects/6/views/1)
- **Project manager:** Owen Koster — sprint planning, partner questions, risks, and checkpoint coordination.
- **AI coordinator:** Charley Pan — `AGENTS.md` and AI-use policies.
- **CI/test documentation contact:** Jun Celvin Malit — exact technical assignments to be confirmed on the project board.
- **Product Owner contact:** Microsoft Teams channel.
- **Code and asset licensing:** No current licensing agreement
