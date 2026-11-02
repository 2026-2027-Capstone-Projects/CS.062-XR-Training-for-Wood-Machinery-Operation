# AGENTS.md

**Owner:** Charley Pan (AI Coordinator). Update this file whenever the stack, repository structure, or AI contribution rules change. This is a navigation and instruction file; do not duplicate the requirements or technical design here.

## Project Context

- **Project:** XR Training for Wood Machinery Operation (OSU CS 461 capstone, CS.062).
- **Purpose:** Build an XR training application for safe operation of wood-processing machinery, initially the selected woodworking press.
- **Engine:** Unity 6000.6.5f1 (Unity 6.6), C#.
- **Mobile build requirements (team decision):** Unity built-in Build Profiles/build pipeline; Unity Hub modules **Android Build Support** (Android SDK & NDK Tools, OpenJDK), **iOS Build Support**, and **macOS Build Support**. iOS device builds additionally need macOS/Xcode and signing.
- **AR package requirements:** **AR Foundation** (`com.unity.xr.arfoundation`), **Google ARCore XR Plug-in** (`com.unity.xr.arcore`), **Apple ARKit XR Plug-in** (`com.unity.xr.arkit`), **XR Plug-in Management** (`com.unity.xr.management`), and **XR Interaction Toolkit** (`com.unity.xr.interaction.toolkit`). Use the standard Unity AR stack rather than creating a custom XR framework. Use package versions compatible with Unity 6000.6.5f1. Maintain consistent package versions through the Unity project's `Packages/manifest.json` and `Packages/packages-lock.json`; update dependencies in reviewed PRs. **[VERIFY installed versions and platform configuration; XR Interaction Toolkit reportedly installed.]**
- **Repository layout:** Current public repository tree places the Unity project at `Unity Project Files/Demo Interactive Application/` (two folder levels below the repository root; verify before checkpoint commit).
- **Tools:** GitHub for source and pull requests, GitHub Projects for Kanban/sprint tracking, Microsoft Teams for communication and partner-shared files.
- **Product Owner:** The project partner participates in weekly in-person reviews and project decisions.
- **Project management:** Owen Koster coordinates the GitHub sprint board, requirements clarification, partner decisions, documentation/checkpoint readiness, and risks/dependencies. This is a **project-management role**, not an assignment as the AR technical implementation owner.
- **Week 10 delivery working plan (project-manager decision, 2026-11-01):** The partner has not specified or confirmed a distribution platform. To provide a concrete Week 10 plan, **Owen Koster provisionally selected Google Play internal testing (Android)** and **Apple TestFlight external beta testing (iOS)** for an installable phone/tablet AR walking skeleton. These are **not partner-approved platforms or live delivery channels**. Public store publication is not part of the plan. **[VERIFY partner/mentor agreement, developer account access, Android/iOS test devices, technical publishers, and release links.]** If the channels prove unsuitable, record and review an alternative instead of silently treating this assumption as final. The full XR scope extends beyond Week 10.

## Read First — Sources of Truth

- [`docs/requirements.md`](docs/requirements.md) — official objectives, constraints, acceptance criteria, non-goals, open questions.
- [`docs/design.md`](docs/design.md) — *current* architecture and code contracts; proposed architecture and deployment decisions are labeled separately.
- [`docs/risks.md`](docs/risks.md) — single ranked project risk register.
- [`README.md`](README.md) — clean-clone and run instructions.
- [`CONTRIBUTING.md`](CONTRIBUTING.md) — development, review, tests, and Definition of Done.
- [Team Charter](https://docs.google.com/document/d/1aCvjpJvoKy4TYI0TW1dogsQggC9_3ZhhFm_XPdR4bxQ/edit?tab=t.0) — team agreements; maintained outside the product repository.
- [docs/] — 3D reconstruction/scanning-tool evaluation. Do not treat proposed RFCs as adopted decisions.

## Rules and Constraints

- Follow standard Unity C# conventions: **PascalCase** for classes and methods, **camelCase** for local variables.
- Write clear commit messages, make focused changes, and follow the branch/PR workflow in `CONTRIBUTING.md`. The default branch is `main`, and merging requires **one teammate approval**.
- Approved AI tools: **ChatGPT, Claude, and Gemini**. Obtain approval from **Charley Pan (AI Coordinator)** before using another AI tool.
- **Never include partner data, credentials, API keys, partner meeting notes, or recordings in AI prompts or code.** Do not upload partner manuals, SOPs, machine imagery, or other restricted materials to an AI tool or public repository without authorization.
- Do not invent project details, APIs, scene paths, tests, safety instructions, or decisions that are not documented and verified.
- **Use Unity's existing AR Foundation / ARCore / ARKit / XR Plug-in Management / XR Interaction Toolkit architecture; do not design a competing XR tracking or interaction framework.** Keep project-specific code focused on woodworking machine controls and training logic, with Unity-stack adapters only when needed.
- **Treat Google Play internal testing (Android) and Apple TestFlight external beta (iOS) as the current provisional Week 10 delivery plan, recorded by the project manager on 2026-11-01.** Do not describe these as confirmed by the Product Owner. Changing the plan requires a recorded project/team decision and partner or mentor discussion; do not silently substitute sideloading or public releases. Do not claim store access or a released build without evidence; never commit signing keys, provisioning secrets or developer credentials.
- Ask a human before changes affecting security, confidentiality, or major design decisions.
- Do not treat manufacturer instructions, SOP notes, or draft training logic as validated safety procedures until the appropriate human reviewer confirms them.
- Update the relevant living documents when code behavior, requirements, architecture, or deployment assumptions change.

## Known Development State — Confirm Against Code

The team reports a Unity scene prototype with `InputMaster` raycast selection and `TranslationInteraction` / `RotationInteraction` UI controls. `InputMaster` updates `SelectedObject` on a collider hit and invokes `Interactable.OnInteract()` when the hit object has that component. The PlayMode integration test `RaycastHit_SelectsCorrectObject` uses a simulated mouse and a collider-equipped cube and **passed locally and in the GameCI XML results (1/1)**. 

Two **independent** GitHub Actions workflows are the selected CI configuration: `.github/workflows/ci.yml` runs `game-ci/unity-builder@v4` targeting `StandaloneLinux64`; `.github/workflows/unity-tests.yml` runs `game-ci/unity-test-runner@v4` in `playmode`. Both are configured for PRs targeting `main`, pushes to `main`, and manual runs. **[VERIFY both workflow files, successful overall job runs on the checkpoint commit, required branch status checks, and a deliberately failing regression run.]** One GameCI XML result has already shown 1/1 tests passed; this does **not** by itself establish that the GitHub Actions job finished successfully. Do not describe unimplemented AR training, procedures, warnings, progress tracking, or logging as completed features.

## Updates and Ownership

- **Project management and checkpoint coordination — Owen Koster:** maintain sprint-board owner/status coverage, coordinate clarification with the Product Owner, track delivery decisions and external prerequisites, maintain the risk register, and ensure the checkpoint PDF/evidence links identify the correct commit. **Owen is not automatically the mobile AR implementation owner.**
- **AI policy and context file — Charley Pan (AI Coordinator):** keep `AGENTS.md` current and approve additional AI tools.
- **Architecture, repository structure, and technical decisions — Charley Pan:** maintain architectural references and link accepted RFCs/ADRs; development owners for individual features must still be assigned through GitHub Issues.
- **Build, tests, lint, and CI documentation — Jun Celvin Malit:** record verified commands and workflow/run links; coordinate the checkpoint automated-test requirement. **[VERIFY actual task assignments with team.]**
- **AR platform integration and test-device builds — [ASSIGN technical owner(s)]:** verify required package versions, enable ARCore/ARKit, validate Android/iOS builds, and, if the provisional delivery plan is confirmed and feasible, publish to **Play Console internal testing / TestFlight external beta** with authorized developer accounts. Owen coordinates scheduling, product-owner agreement and milestone evidence as PM, not the implementation by default.
- **Tool-specific AI files (e.g. `CLAUDE.md`):** point to `AGENTS.md` rather than copying rules into a competing file.
