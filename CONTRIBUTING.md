# CONTRIBUTING.md

Contribution guidelines for **XR Training for Wood Machinery Operation** (OSU CS 461 capstone, CS.062).

**Related resources:** [GitHub repository](https://github.com/2026-2027-Capstone-Projects/CS.062-XR-Training-for-Wood-Machinery-Operation) · [Team Charter](https://docs.google.com/document/d/1aCvjpJvoKy4TYI0TW1dogsQggC9_3ZhhFm_XPdR4bxQ/edit) · [README](README.md) · [AGENTS.md](AGENTS.md)

This document describes the team workflow and its intended checks. A policy is not an automated gate unless GitHub or another tool actually enforces it. 
## 1. Development setup

1. Obtain repository access and access to the team's Microsoft Teams communications and authorized project documents.
2. Clone the repository:

   ```bash
   git clone https://github.com/2026-2027-Capstone-Projects/CS.062-XR-Training-for-Wood-Machinery-Operation.git
   ```

3. Install **Unity 6000.6.5f1** through Unity Hub and open `Unity Project Files/Demo Interactive Application/`.
4. Use Visual Studio for C# development and its built-in static analysis feedback. The team's selected Unity Hub modules are Android Build Support (including SDK & NDK Tools and OpenJDK), iOS Build Support, and macOS Build Support. The Linux build CI currently uses Unity's built-in build pipeline. iOS device builds additionally need macOS, Xcode, an Apple developer account, and signing.
5. Use compatible versions of the selected Unity packages: **AR Foundation**, **Google ARCore XR Plug-in**, **Apple ARKit XR Plug-in**, **XR Plug-in Management**, and **XR Interaction Toolkit**. Manage package versions through the Unity project files; review changes to `Packages/manifest.json` and `Packages/packages-lock.json` in pull requests. 
6. Follow the [README quickstart](README.md) to launch the current scene. 

The team plans to use standard Unity AR functionality rather than build a custom XR stack. **Delivery is not partner-confirmed:** on **2026-11-01**, project manager **Owen Koster** provisionally chose **Google Play internal testing (Android)** and **Apple TestFlight external beta (iOS)** as a practical Week 10 release approach. This is a **working delivery assumption**, not a platform requirement issued by the partner. Partner/mentor agreement, publishing access, device testing, and release links still require confirmation. If the plan changes, record the new decision and update the [deployment plan](docs/design.md) before release.

## 2. Build, testing, lint, and security

Build and test execution use **separate GitHub Actions workflows**, each with its own checkout:

| Check | How to run / where enforced | Current evidence or limitation |
| --- | --- | --- |
| Unity Linux build | `.github/workflows/ci.yml`, GameCI Unity Builder, `StandaloneLinux64` | [Reported build run](https://github.com/2026-2027-Capstone-Projects/CS.062-XR-Training-for-Wood-Machinery-Operation/actions/runs/41378193143); **[VERIFY passed at checkpoint commit]** |
| Unity PlayMode integration test | `.github/workflows/unity-tests.yml`, GameCI Unity Test Runner, `playmode` | `RaycastHit_SelectsCorrectObject` reported **1/1 passed** in GameCI XML; [reported test run](https://github.com/2026-2027-Capstone-Projects/CS.062-XR-Training-for-Wood-Machinery-Operation/actions/runs/41378193105).|
| Local testing | Unity: **Window > General > Test Runner > PlayMode**; run `RaycastHit_SelectsCorrectObject` or all relevant PlayMode tests | Test covers simulated click, raycast selection via actual `InputMaster`, and selected-object assertion |
| Lint / static analysis | Visual Studio built-in analysis plus human review | No separate automated lint gate confirmed |
| Confidentiality and security | PR review for secrets, signing keys, licensing, and restricted partner material | No automated security workflow confirmed |
| Manual smoke test | Run affected Unity scene and check behavior | Check that the scene looks correct and has no obvious issues |

Commit test scripts, assembly definitions (`.asmdef`), and relevant Unity `.meta` files along with production changes.

**Regression demonstration for Repo Checkpoint 1:** On a disposable branch, keep the existing test assertion expecting the clicked cube. Temporarily break the **production selection behavior** (for example, replace the `InputMaster` assignment with `selectedObject = null`), run the PlayMode-test workflow, and save its failing-run URL. Restore the production code, then confirm the same test passes and save its successful-run URL. **Do not merge the intentionally broken version.** Merely changing the expected assertion to force a failure does not demonstrate that a test catches a broken feature.

The team intends to require successful status checks named **Build Unity Project** and **Unity PlayMode Tests**, plus one teammate approval, before merging into `main`.

## 3. Branching and commits

- `main` is the default branch. Make changes on feature or fix branches and open pull requests; do not commit directly to `main`.
- Each student uses their own GitHub account for code contributions. Reference relevant issues and GitHub Project tasks in PRs.
- Make focused commits whose messages explain **what changed and why**. Squash noisy branches as appropriate to keep history reviewable.
- Never force-push `main` or commit live credentials, Unity-generated cache/build directories, dependency directories, or large binaries outside Git LFS.
- Re-run relevant checks after resolving Unity scene merge conflicts.

## 4. Pull requests and review

- Require **one teammate approval** before merging.
- Include the linked issue/task, summary of code changes, tests run, affected scenes, and documentation updates in each PR.
- Reviewers check functionality, maintainability, safety and security implications, confidentiality, and consistency with `docs/requirements.md` and `docs/design.md`.
- Aim for a review **within 48 hours** once the PR is ready; communicate delays on Teams.
- Never treat a passing compilation check as validation of machinery operating or safety procedures; those require an appropriate human review against authorized documentation.

## 5. Definition of Done

A change is ready to merge when:

- [ ] The appropriate automated tests and existing CI build complete successfully.
- [ ] A teammate has reviewed and approved the PR.
- [ ] Any affected product documentation and tests are updated in the same PR.
- [ ] Visual Studio code-analysis feedback is addressed and the reviewer has checked for secrets, confidential partner material, and unsafe training assumptions.
- [ ] Changes to procedures or safety instructions have been reviewed against authoritative material by the appropriate designated person, **once that review responsibility is assigned**.
- [ ] GitHub Issue / Project status and contribution record are updated.

**Planned gates not yet verified:** automated lint and security scanning are not confirmed. The team must verify both required workflow statuses and branch protection before describing them as enforced.

## 6. Contribution norm and traceability

**Every student should own at least one piece of accepted work per sprint**, as required by the team charter assignment. Plan tasks small enough to review within a sprint, and record accepted work in each student's Sprint Notes *Contribution* line.

- **Code:** accepted after a reviewed PR merges and required CI checks pass; attribution is through the author's GitHub account and linked issue.
- **Tests, documentation, and design decisions:** accepted after an appropriate teammate reviews the work and it is recorded in the relevant PR, issue, or decision document. For repository documents, changes ordinarily follow the reviewed PR workflow.
- **Project coordination, partner clarification, and other non-code work:** accepted when the assigned issue or shared team record contains a concrete deliverable (such as reviewed meeting outcomes, clarified requirements, sprint-board updates, or checkpoint evidence) and another teammate confirms completion.
- **Pairing:** name both contributors and describe each person's contribution in the relevant issue/PR and sprint notes; pairing does not remove the individual traceability requirement.

**[VERIFY that these acceptance rules match the Team Charter and the team's established contribution policy.]**

## 7. Where work lives and how to get help

- **GitHub Projects / Issues:** sprint work, owners, statuses, and technical tasks. Owen Koster coordinates project management and checkpoint evidence. [GitHub project board](https://github.com/orgs/2026-2027-Capstone-Projects/projects/6/views/1)
- **GitHub PRs:** code changes and review history. Technical feature owners are assigned per issue.
- **Repository:** `docs/requirements.md`, `docs/design.md`, `docs/risks.md` (the single project risk register), `README.md`, `CONTRIBUTING.md`, `AGENTS.md`, and technical RFCs/ADRs.
- **Microsoft Teams:** team questions, partner communications, weekly reviews, and access-controlled project documents.
- **Outside the product repository:** the Team Charter, retrospectives, Definition of Shipped, nontechnical incident records, and partner-designated handoff documents.

Owen Koster coordinates project management; Charley Pan is AI Coordinator and architecture-documentation contact; Jun Celvin Malit is the named CI/testing documentation contact. Ask for technical help in Teams or the relevant GitHub issue. Refer sensitive interpersonal matters to the team process or TA, not the technical risk register.

## 8. Provisional mobile distribution and rollback

- **Android (provisional PM plan):** the team intends to upload a signed `.aab` through an authorized Google Play Console account to the **internal testing** track and verify installation through a tester opt-in link.
- **iOS (provisional PM plan):** the team intends to build/sign using Unity's iOS Build Profile and Xcode, then distribute through **TestFlight external beta** to a non-team tester, allowing for Apple beta review.
- Exercise changes on compatible test devices before outside distribution; retain a known-good signed build and document how to withdraw or supersede a bad beta. 
- **Decision status:** Owen Koster recorded these channels as his preferred Week 10 approach on **2026-11-01**; Product Owner/mentor agreement and feasibility are **not confirmed**. Any replacement channel must be documented with a revised decision and reported to the Product Owner/mentor.
- Publishing or changing signing and store settings requires human review; do not commit keys, credentials, or provisioning secrets.

## 9. AI and confidentiality

Read [AGENTS.md](AGENTS.md) before AI-assisted work. Approved AI tools are **ChatGPT, Claude, and Gemini**; additional tools require approval from **Charley Pan (AI Coordinator)**. Do not send partner data, manuals, SOPs, imagery, meeting notes or recordings, credentials, keys, or other restricted content to AI tools without appropriate authorization. All AI-assisted changes still require normal tests and human review.

**Before submitting Repo Checkpoint 1:** verify the final text against the actual repository and Team Charter, replace `[VERIFY]` and `[ASSIGN]` markers where possible, and ensure the PDF renders the version committed at the full SHA named on its cover.
