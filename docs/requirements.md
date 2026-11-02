# Project Requirements — Wood-Processing Machine XR Training System

**Status:** Living document; draft for Fall 2026, Week 5 repository checkpoint  
**Product owner:** Project partner (Raffaele De Amicis)  
**Initial machine:** Woodworking press covered by the supplied manual and SOP  
**Project manager:** Owen Koster (sprint coordination, requirements clarification, milestone tracking)

> **Status key:** **Confirmed objective** = explicitly stated in the official project requirements; **reported current** = described by the team but not verified at the checkpoint commit; **proposed acceptance** = testable interpretation for partner review, not an approved final specification. The full-project requirements below are not a claim that the features are already built.

## 1. Problem and context

Industrial woodworking and wood-processing machinery has complex controls, startup/shutdown procedures, material-handling sequences, and safety hazards. Training directly on physical machinery consumes instructor time, can interrupt normal work, and may expose inexperienced operators to avoidable risks. Wood-products educators and industrial trainers need a practice environment where students and new operators can learn machine components and controls, rehearse correct operating and safety procedures, and receive feedback before working on the physical equipment.

**Official project goal:** Develop an **XR training system** representing the selected woodworking machine and its workspace, with guided interactive procedures, safety feedback, trainee progress tracking, event logging, and a reusable software foundation for future research and training.

**Initial content sources:** Manufacturer operating/instruction manual, a graduate-student SOP, and notes made during a live operation/SOP review, all shared through Teams. 

**Fall Week 10 delivery working plan (project-manager decision, 2026-11-01; not partner confirmed):** An installable **phone/tablet AR prototype on Android and iOS** is the working milestone assumption. Because the Product Owner has **not specified or agreed to a delivery platform**, Owen Koster (project manager) has **provisionally chosen Google Play internal testing (Android)** and **Apple TestFlight external beta testing (iOS)** as the intended channels. This is a project-management delivery proposal, **not an official partner requirement or verified release**. The prototype should allow someone outside the development team to interact with the machine representation. This Week 10 walking skeleton is not completion of the full-project objectives.

## 2. Stakeholders and users

- **Product owner:** Partner who provides requirements, attends weekly meetings and reviews decisions.
- **Primary users:** Students, inexperienced machine operators, and trainees.
- **Training stakeholders:** Wood-products educators and industry trainers who need a way to familiarize learners with equipment without initially using the physical press.
- **Implementation team:** Student developers using Unity/C#, GitHub and Teams.
- **Project manager:** Owen Koster coordinates the backlog, partner clarification requests, risk follow-through, and checkpoint evidence; AR/XR coding ownership is assigned separately.

## 3. Functional requirements — full project objectives

Every requirement FR-01 through FR-12 below comes directly from the **official project objectives**. Acceptance criteria are **proposals** for making those objectives testable; confirm details before treating them as a signed-off specification.

| ID | Confirmed requirement | Proposed acceptance criterion (to validate with product owner) |
| --- | --- | --- |
| FR-01 | Provide an XR representation of the selected woodworking machine. | A trainee can display a recognizable 3D model and inspect the training-relevant machine components. **[VERIFY required components and fidelity.]** |
| FR-02 | Represent the machine's relevant working environment. | The training scene includes the workspace and context needed to complete the selected procedures. **[VERIFY required features and dimensions.]** |
| FR-03 | Provide interactive machine controls and components. | A user can activate each selected training control, and a supported action produces a defined model or training-state response. **[VERIFY controls/actions.]** |
| FR-04 | Provide a structured startup procedure. | The trainer presents an ordered, reviewed startup sequence; completing its required actions advances the procedure and reports completion. **[VERIFY individual steps and safety prerequisites.]** |
| FR-05 | Provide a structured operating procedure. | The trainer guides the user through a selected normal operating sequence and recognizes required user interactions in order. **[VERIFY selected scenario and expected machine behavior.]** |
| FR-06 | Provide a shutdown or safe-state procedure. | The user can complete a reviewed shutdown/safe-state sequence and receive a defined end state and completion indication. **[VERIFY sequence and what safe state means for this machine.]** |
| FR-07 | Provide safety checks and warnings. | For a reviewed safety prerequisite, the system prompts the user to perform the check and warns when that prerequisite is not met. **[VERIFY safety rules and wording.]** |
| FR-08 | Detect procedural errors or unsafe actions. | At least one known out-of-sequence or unsafe simulated action is recognized and classified as an error, without silently advancing the procedure. **[VERIFY error cases and consequences.]** |
| FR-09 | Provide instructional prompts and user feedback. | The user receives step instructions and an observable response to valid and invalid actions, including corrective feedback when appropriate. **[VERIFY presentation and accessibility expectations.]** |
| FR-10 | Track task completion, errors, and training progress. | A training session exposes completed tasks, detected errors and current progress for review. **[VERIFY progress definition, scoring, and whether data persists across sessions.]** |
| FR-11 | Provide a data-logging capability for later analysis. | At least one run produces retrievable records of meaningful procedure/action outcomes. **[VERIFY required fields, export format, storage, privacy and retention.]** |
| FR-12 | Support reusable training architecture for future procedures or machines. | A second sample procedure can reuse the interaction/procedure/feedback system without duplicating its core implementation. **[VERIFY acceptable extension process.]** |

**FR-13 — Technical and user documentation (confirmed deliverable):** The repository contains maintained technical documentation and usable instructions for running and using the application. **Proposed acceptance:** a new contributor can follow the README from a clean clone, while a trainee/reviewer can follow the user instructions for the delivered build. 

### Reported current prototype (not confirmation of full-project requirements)

Team-reported code includes raycast-based selection (`InputMaster`), position editing (`TranslationInteraction`) and rotation editing (`RotationInteraction`) through TextMeshPro UI. For the selection flow, `InputMaster` reads the `Player.Attack` input, raycasts from `Camera.main` inside `FixedUpdate()`, assigns the collider's GameObject to `SelectedObject` on a hit, and invokes `Interactable.OnInteract()` when present. The PlayMode integration test `RaycastHit_SelectsCorrectObject` passed locally and was reported as **1/1 passed** by the GameCI XML result. **[VERIFY the scripts and test at the checkpoint commit and the complete GitHub Actions job status.]** These are **development interactions**, not evidence that training startup, real machine controls, mobile AR, procedure evaluation, progress tracking, or logging already work.

## 4. Non-functional requirements

| ID | Requirement | Verification approach / remaining decision |
| --- | --- | --- |
| NFR-01 | Architecture is modular and maintainable, suitable for future training and research (official goal). | Review separations among machine representation, interaction, procedures, feedback and logging; test reuse of a procedure. 
| NFR-02 | Procedures and safety feedback are accurate enough for training use (inferred quality requirement from official safety objectives). | Designated reviewer checks each implemented sequence and warning against an agreed authoritative operating source. |
| NFR-03 | Instructions and corrective feedback are understandable to new operators (inferred usability requirement). | Walk through scenarios with product owner/representative users; record confusing steps. |
| NFR-04 | Repository builds and automated tests are repeatable (course checkpoint requirement). | Independent GitHub Actions build and PlayMode-test workflows run on PRs into `main`. `RaycastHit_SelectsCorrectObject` makes a real selection assertion. Demonstrate a deliberate code regression that makes CI fail and confirm both workflows pass on the checkpoint commit. |
| NFR-05 | Under the **provisional PM delivery plan**, the Week 10 AR build is distributable to outside-team testers through Google Play internal testing (Android) and Apple TestFlight external beta testing (iOS); the platforms and channels are **not partner-confirmed**. | From the named testing channels, at least one invited non-team tester on each supported platform can install, launch and complete a guided interaction on a compatible phone/tablet; retain working opt-in/invitation links and a test record. |
| NFR-06 | Logging is appropriate for later analysis without unnecessarily collecting personal information (proposed data-management constraint). | Agree schema, location, access, anonymization and retention before collecting identifiable training data. **[DECISION NEEDED.]** |

## 5. Constraints (externally imposed, not team choices)

1. The product owner has selected **Unity and C#** for development.
2. The project focuses initially on a specific piece of wood-processing equipment and its real operating/safety procedures. **[VERIFY exact press model.]**
3. The partner has provided the manufacturer manual, SOP and live-change notes through **Microsoft Teams**; source authority has not been resolved.
4. The project must support future training activities and research, not merely a one-off static machine model (official objective).
5. Project communication and shared partner documents use **Teams**; GitHub is used for source code, issue tracking and visibility; weekly in-person meetings are expected.
6. The team has personal computers and may use laboratory computers, XR devices, tablets and capture equipment when available; device access must be arranged.
7. **[VERIFY]** Permissions governing publication of partner-provided manuals, imagery, footage, and scans. Until confirmed, avoid publishing them in public version control or external tools.

**Team implementation choices and provisional delivery assumption (not imposed constraints):** Unity version **6.6 (6000.6.5f1)**, the repository layout, Unity's built-in build pipeline, platform build modules, and Unity's standard AR Foundation/ARCore/ARKit/XR Plug-in Management/XR Interaction Toolkit stack are team technical choices. **Phone/tablet AR for Android and iOS, with Google Play internal testing and Apple TestFlight external beta as Week 10 channels, is a separate provisional delivery decision recorded by Owen Koster (PM) on 2026-11-01, pending Product Owner/mentor agreement.** None of these delivery specifics came from the official partner objectives.

### Mobile AR development requirements (team-selected)

- **Build workflow:** Unity's built-in **Build Profiles/build pipeline**.
- **Unity Hub modules:** **Android Build Support** with **Android SDK & NDK Tools** and **OpenJDK**; **iOS Build Support**; and **macOS Build Support**. For local iOS builds, provide **macOS/Xcode** and Apple signing credentials.
- **Required Unity AR packages:** **AR Foundation** (`com.unity.xr.arfoundation`) for cross-platform AR functionality; **Google ARCore XR Plug-in** (`com.unity.xr.arcore`) for Android; **Apple ARKit XR Plug-in** (`com.unity.xr.arkit`) for iOS; **XR Plug-in Management** (`com.unity.xr.management`) for platform providers; and **XR Interaction Toolkit** (`com.unity.xr.interaction.toolkit`) for interaction components. **The team has selected this standard Unity stack rather than building a custom XR framework.** This is an agreed implementation constraint for the team, not an externally imposed product requirement.
- **Implementation boundary:** Reuse Unity-provided AR tracking, session management, provider integration, and interaction infrastructure. Write project-specific code for woodworking machine controls, training steps, safety checks, feedback, progress, and logs; only add specialized AR interaction code where the built-in components need an application-specific adapter. Do not develop an alternative tracking/provider/framework stack without a documented team design decision.
- **Compatibility acceptance (proposed):** The target Unity project imports all five packages without errors; the Android build enables ARCore and the iOS build enables ARKit; the chosen XR Interaction Toolkit components can handle an example interaction; at least one supported device on each intended platform launches an AR session. **[VERIFY package versions, enabled providers, interaction setup, devices and builds before claiming this passes.]**

### Provisional mobile AR delivery approach (PM decision for Week 10; not partner-approved)

- **Android production target:** Google Play **internal testing**, using a signed Unity-generated Android App Bundle (`.aab`) distributed through an authorized Play Console account. A tester outside the student team must be able to opt in and install the build using the Play testing link. This is a private test track, **not** a public Play Store launch. **[VERIFY account owner, signing access, test group and link.]**
- **iOS production target:** Apple **TestFlight external beta testing**, using a signed iOS build uploaded via Xcode/App Store Connect on macOS. A tester outside the student team must be able to install and launch the build through a TestFlight invitation or approved public link. External beta distribution generally requires Apple's beta review, so submission and review time must be planned. **[VERIFY Apple Developer team/account owner, Mac/Xcode, signing, review and invitation link.]**
- **Shared acceptance:** Each channel has a versioned release corresponding to the team's Week 10 milestone and evidence that it runs on a compatible non-team phone/tablet. Record tester access steps and channel URLs for the checkpoint evidence. A Unity Editor demo or source-only repository is insufficient for this mobile milestone.
- **Decision and agreement caveat:** Google Play internal testing and TestFlight external beta are a **provisional project-manager choice recorded 2026-11-01**, not a partner-confirmed delivery requirement or an active distribution system. Confirm the Product Owner/mentor agrees and determine account owners, platform access, supported devices, and test invitations before treating this as a settled milestone. Public store release is not assumed.

Official channel guidance: [Google Play testing tracks](https://support.google.com/googleplay/android-developer/answer/9845334) and [Apple TestFlight](https://developer.apple.com/testflight/).

### Checkpoint 1 automated-test acceptance (course requirement)

The project uses two independent GitHub Actions workflows: `ci.yml` for a `StandaloneLinux64` Unity build, and `unity-tests.yml` for Unity PlayMode tests. The current PlayMode integration test `RaycastHit_SelectsCorrectObject` checks the **existing** `InputMaster` raycast-selection behavior using a collider-bearing `Interactable` cube; it does not test mobile AR or the official machine-training workflows. A local run passed, and GameCI's `playmode-results.xml` reported **1 of 1 passed**. The first combined workflow had publishing/dirty-worktree issues, leading the team to choose separate workflows. **[VERIFY green status of both separate workflows at the named commit.]** The team is performing a deliberate failing regression test on a separate branch; **[ADD failing run URL and evidence after it actually fails]**. Never merge the intentionally broken production code.

## 6. Success measures and milestone distinction

### Full-project success (official objectives)

The delivered system demonstrates the machine/workspace, interactive components, structured **startup**, **operation**, and **shutdown/safe-state** procedures; supports safety checks, errors, prompts, feedback, progress tracking and analyzable logs; and provides a reusable architecture plus technical/user documentation. Exact scenario counts, machine detail, data structure, and supported XR hardware remain **[VERIFY]**.

### Fall Week 10 walking skeleton (provisional PM delivery assumption; not final scope sign-off)

A **project-manager-proposed two-platform mobile AR walking skeleton**, intended for **Google Play internal testing** (Android) and **Apple TestFlight external beta testing** (iOS), **subject to Product Owner/mentor agreement and channel feasibility**. A non-team tester on each platform should be able to install and launch the app, see a machine representation, perform at least one meaningful interaction and receive feedback for a small guided procedure fragment. **[PROPOSED acceptance; confirm scope with Product Owner.]** This is the narrowest end-to-end demonstration for Week 10, **not** a substitute for FR-04 through FR-13 at the end of the entire project.

Week 10 evidence should include both testing-channel links/invitations, outside-team installation and demonstration records, and the product-owner agreement. **[VERIFY channel/account owners, agreement, tester access and dates.]** See `docs/design.md`.

## 7. Non-goals and scope boundaries (proposed; verify with product owner)

- Sending commands to the physical press or replacing the physical machine's safety systems.
- Granting safety certification or replacing required hands-on supervision.
- Engineering-grade simulation of every internal mechanical or material property.
- A production learning-management system, user-account platform or certification service.
- All possible XR hardware configurations in the initial Week 10 build.

**Important:** Recording session outcomes and training progress is **not** a non-goal; the official project objectives expressly require it. An account-based learner database, however, has not been specified.

## 8. Open questions and responsibilities

| Open question | Responsible person | Evidence of resolution |
| --- | --- | --- |
| How should task completion, errors and progress be represented? | **[ASSIGN developer]**; Owen Koster tracks decision | Accepted session/progress definition and test cases |
| What log events/fields, export, storage, retention and privacy rules are needed for later analysis? | **[ASSIGN developer]**; Owen Koster coordinates partner clarification | Logging decision/design review |
| Can training sequences be configured and reused for additional procedures/machines? | Charley Pan (architecture coordination; **[VERIFY]**) | Approved architecture decision and second scenario test |
| Which compatible versions of AR Foundation, ARCore, ARKit, XR Plug-in Management and XR Interaction Toolkit are actually installed/configured, and do the agreed devices run AR sessions? | **[ASSIGN AR technical owner]**; Owen Koster tracks readiness | Project package manifest, Android/iOS XR settings and device smoke tests |
| Will the Product Owner/mentor agree to Owen Koster's provisional 2026-11-01 choice of Google Play internal testing and Apple TestFlight external beta as the Week 10 target? | Owen Koster (PM) | Dated meeting decision; if declined, documented alternative and revised design |
| Which developer accounts/owners, signing credentials, testers and review timelines support Google Play internal testing and TestFlight external beta delivery? | Owen Koster (PM coordination); platform publishers **[ASSIGN]** | `docs/design.md` deployment section, Play Console/TestFlight channel links and release/test records |
| Which photogrammetry approach and asset format should be used? | **[ASSIGN RFC evaluator]**; Owen Koster tracks decision | RFC outcome and scan-to-Unity test |
| What partner material may be included in public version control or AI tools? | Owen Koster (PM liaison) / Charley Pan (AI use) | Recorded partner guidance |

## 9. Sources of truth and change management

- **Official partner project requirements:** problem statement and objectives shared by the user; reference link/date **[FILL]**.
- **Partner's additional written clarifications:** Unity/C#, XR focus, weekly meeting cadence, product owner role and equipment/document availability.
- **Machine operation sources:** manufacturer manual, graduate-student SOP, live-session notes in Teams; exact revision information.
- **Repository:** code, tests, PRs and CI at the full commit SHA recorded on the checkpoint PDF cover.

Update requirements and corresponding technical design when decisions or code change. A project objective is not considered implemented until evidence from code, tests or demonstration supports that claim.
