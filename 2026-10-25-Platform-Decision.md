# RFC: Development Platform Strategy for XR Wood Machinery Operation Training

| Field | Value |
| --- | --- |
| Author | Jun Celvin Malit |
| Date | 2026-10-25 |
| Status | Proposed |
| Decision Date | (N/A) |
| Deciders | (N/A) |
| Superseded By | N/A |

## Problem and Context

We are developing an XR training program in order to safely train users to operate wood machinery. The team needs to decide what platforms to prioritize during development. This decision is important because it affects the technologies required for implementation, including the libraries we use for porting our program into a shippable product. This also impacts how much work is required when the project is eventually expanded to additional platforms.

The decision is needed now because platform assumptions directly influence how we design user interaction around the virtual environment. Developing a UI centered around one platform would mean the program and corresponding interaction scripts will need a large rehaul in order to support the new platform

## Options

At least three options, **including "do nothing"**. Give each option a short name, an account of its real costs, and the condition under which it would be the right choice. An option with no such condition is a strawman, and a strawman weakens the whole document.

### Option A: VR Centered Development

The team chooses to develop primarily for virtual reality devices from the beginning, designing training interactions around users on a Meta Quest 3 or other VR devices. 

The main benefit is that the team will have a technologically more compelling and challenging task to be visually and tactically more effective for training, since users will be able to imitate the bodily motions required to operate machines instead of interacting with a smaller scale device.

The cost is significantly increased complexity in terms of development and testing, since we would be required to immediately jump into developing on an unfamiliar platform at the same time as figuring out what the training process will look like. 

### Option B: Desktop-first webapp development

The team develops the initial testing experience as a desktop web application, with tablet portability being considered during development due to the closeness of the two development-wise. 

The primary benefit is that the team will be able to focus on the quality of the training and instructions without immediately tackling the complexity of VR development. It is also more accessible for users due to not requiring a costly VR headset; significantly more users own a laptop or mobile device than VR devices.

### Option C: Do Nothing

Doing nothing means developing user interaction and UI components around a mix of all of our desired platforms. This would reduce the risk of choosing a platform too difficult for us to implement on first, instead leaving our options open to pick whichever option seems easiest first as the project develops. However, this will harm our organization because our deliverables will have a mix of testable and untestable features since neither implementation will be focused. This will mean having multiple branches for different devices that will be at different stages of development. 

## Analysis

The technical tradeoffs that matter for this decision: performance, complexity, maintainability, security, cost. Argue from evidence (benchmarks, prototypes, documentation, prior art) and cite what you relied on so a reviewer can check it. Acknowledge what you are uncertain about.

Undeniably, we feel like the largest concern with choosing a platform to start on is deciding when would be best to handle the complexity that comes with developing on VR. Although focusing on easier, more traditional devices like PCs would provide us the opportunity to test training content faster, it would also require us to place more effort into redesigning the program for VR systems.

“Furthermore, having a non-light headset on and trapping eyes into a high-temperature zone which would make eyes dry out faster is undoubtedly uncomfortable and frustratingly made VR players stay in VR for usually no longer than one to two hours” https://www.researchgate.net/publication/381529689_Virtual_Reality_Challenges_of_VR_Game_Development_for_Emerging_Game_Studios

Using VR as technology for training also brings up different constraints. Ensuring the process is as streamlined as possible and able to be completed in bursts that allow for breaks is an issue that worsens when it comes to the discomfort introduced by VR headsets. Overall, we want to be careful with tackling an unfamiliar platform to prevent duplicating development effort whenever possible.

## Recommendation and Verification Plan

Which option you propose and why it wins the analysis above. Then, concretely: how will the team know it worked? List the tests, metrics, or acceptance criteria that would detect failure, and when they will be checked.

I would propose option A because it will allow us to focus on the core training logic of the program. The main reason for this recommendation is that we can create one testable, complete training scene before taking on the additional complexity of VR, giving us a usable, presentable demo build much quicker to be able to identify any issues with the training experience. 

The team will know it worked if, upon finishing the first training scene and moving onto VR development, we can verify that:
	The complete training scene can be used successfully on a desktop environment.
	The core training sequence can be separated from platform specific input without duplicating development efforts (having to create an entirely separate training sequence).
	The training scene can be ported to mobile devices without fundamentally changing core training logic and steps for the user to follow.
	The effort required to adapt the scene to VR is reasonable compared to the effort that would be required to create a standalone product for VR that doesn’t reuse a significant portion of game assets.

## Constraints

Licensing bears on this decision because any frameworks, libraries, or SDKs required as a third party technology to develop on specific platforms must have licensing terms compatible with the project and its intended use.

## Delegation and Validation

AI was not used for the analysis portion since this decision relies directly on the team’s experience and comfort as developers.

## Decision
PROPOSED
