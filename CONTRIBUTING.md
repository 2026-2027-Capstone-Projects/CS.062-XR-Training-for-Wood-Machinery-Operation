### Setup and Workflow
So far, when a new contributor joins, they will get access to the GitHub, download version 6000.6.5f1 of Unity, and get the project installed locally via GitHub. We don’t currently have all the tools we will use figured out yet so this will change
No info on how we will run tests yet, will use the built-in Visual Studio linter
If a teammate is working on a separate feature they should use a branch and merge it together once their feature is tested and passes CI, after merge the project should still pass the CI tests. Commits should be frequent and have descriptive comments for what they are for records. 
Code reviews should look for common issues in the code and should be completed within 48 hours of code completion and communication with teammates. 
For help, ask questions on Teams. 
Team Charter: https://docs.google.com/document/d/1aCvjpJvoKy4TYI0TW1dogsQggC9_3ZhhFm_XPdR4bxQ/edit?tab=t.0 
Team GitHub Repository:
https://github.com/2026-2027-Capstone-Projects/CS.062-XR-Training-for-Wood-Machinery-Operation 
### Definition of Done

For changes to be merged, they must:
 - Pass required tests showing the feature or update works
 - Pass Lint and security checks
 - Have updated documentation for the feature being merged in
 - Pass teammate review and approval. 

Don’t have CI checks, hooks, or tests yet; will be added by Owen during the first sprint


### Contribution Norm

Work is tracked through a KANBAN on GitHub Projects. 
Work is accepted when code tasks are completed and have passed CI tests/ other required tests depending on the code task. Documentation is marked as complete when it has been reviewed by at least one teammate. 
Reviews should be completed within 72 hours of code submission and ideally within 24 hours. 
We will record contributions in sprint notes.
