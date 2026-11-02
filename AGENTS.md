# AGENTS.md

## Project
XR Training for Wood Machinery Operation (OSU CS 461, Project 62).
Engine: Unity 6000.6.5f1 (Unity 6.6), C#.
Unity project: `Unity Project Files/Demo Interactive Application/`.

Requirements: `docs/requirements.md`.
Architecture and code contracts: `docs/design.md`.
Open risks: `docs/risks.md`; flag if a task touches one. Do not silently change risk status.
Development setup: `README.md`.
Team development rules and Definition of Done: `CONTRIBUTING.md`.

Use Unity's standard AR stack: AR Foundation, ARCore, ARKit, XR Plug-in Management, and XR Interaction Toolkit. Do not create a competing XR framework.

## Commands
Check `README.md` and `CONTRIBUTING.md` for verified setup, build, and test commands.
Unity Editor: use the project version specified above.
CI workflows:
- `.github/workflows/ci.yml` — Unity build (`StandaloneLinux64`).
- `.github/workflows/unity-tests.yml` — PlayMode tests.

Both workflows must be verified on the relevant commit before reporting CI success. A passing local test or GameCI XML result alone does not establish a successful CI run.

Do not invent commands or report unverified builds/tests as passing.

## Work
GitHub Issues are the source of truth for feature assignments and implementation tasks.
GitHub Projects tracks sprint and Kanban progress.
Every feature should have an assigned owner and a corresponding issue.

Follow the branch and pull request workflow in `CONTRIBUTING.md`.
Default branch: `main`. Merging requires one teammate approval.

The project partner is the Product Owner and participates in weekly reviews. Record partner decisions and distinguish confirmed requirements from team assumptions.

Week 10 delivery currently has a provisional plan:
- Android: Google Play internal testing.
- iOS: Apple TestFlight external beta testing.

These platforms have not been partner-approved. See the project documentation and confirm prerequisites before treating this plan as final.

## Conventions
- Follow standard Unity C# conventions: `PascalCase` for classes and methods; `camelCase` for local variables.
- Prefer existing Unity APIs and project architecture over custom implementations.
- Keep changes focused and consistent with existing repository structure.
- Update relevant documentation when code behavior, architecture, requirements, or decisions change.
- Do not treat proposed RFCs or unreviewed designs as adopted decisions.

## Boundaries
- Always: Inspect existing code and documentation before implementing. Verify changes with available tests and CI. Report exactly what was and was not verified.
- Ask first: Adding dependencies; changing architecture, schemas, authentication, deployment, or major project decisions; using an unapproved AI tool.
- Safety: Training procedures, warnings, and machine-operation guidance require appropriate human validation. Never present unreviewed instructions as validated safety procedures.
- Privacy: Never include partner data, credentials, API keys, meeting notes, or recordings in AI prompts or code. Do not upload partner manuals, SOPs, machine imagery, or restricted materials to AI tools or public repositories without authorization.
- AI tools: Approved tools are ChatGPT, Claude, and Gemini. Charley Pan (AI Coordinator) must approve additional tools.
- Never: Commit directly to `main`, fabricate project requirements or implementation status, commit credentials/signing keys, or silently change partner-facing decisions.

## Ownership
- AI coordination and this file: Charley Pan.
- Project management, sprint coordination, partner decisions, and risk tracking: Owen Koster.
- Architecture and technical decision documentation: Charley Pan.
- Build, testing, and CI documentation: Jun Celvin Malit.
- Feature implementation: Assigned through GitHub Issues; do not assume ownership based on project-management roles.

For detailed team responsibilities and agreements, see the Team Charter.
