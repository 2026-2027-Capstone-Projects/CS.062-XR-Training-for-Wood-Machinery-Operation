<!-- docs/rfcs/2026-10-25-3DScanTool.md -->

# RFC: [3DScanTool]

<!-- Proposed -->

| Field | Value |
| --- | --- |
| Author | Owen Koster |
| Date | 2026-10-25 |
| Status | Proposed |
| Decision Date | NO DECISION YET |
| Deciders | NO DECISION YET |
| Superseded By | N/A |

## Problem and Context

One part of our project is to simulate a piece of wood machinery in Unity, and to do this, we need to create a 3D model of the machine. We can either use a 3D scanning tool to create our models or hand-model the machine. The project partner encouraged us to use a 3D scanning tool but left the choice up to us. Our team needs to decide which 3D scanning tool we plan to use. 

We need to make this decision so that we can iron out our workflow and begin creating 3D objects to use in Unity. If we don’t decide, we will lose valuable time that could have been spent creating and processing scans for Unity. This also limits our ability to work on the Unity project since we need 3D objects to simulate. This decision is also difficult because if we need to change the tool we use, we will have to rework large parts of our scanning workflow. Depending on the tool, we may have to rescan some objects, spend a large chunk of time cleaning scans manually, and we will also have to spend time learning a different tool. 

## Options

### Option A: [Polycam]
Polycam is a 3D scanning tool that lets users take photos and videos and convert them into a 3D model; it offers free photogrammetry and paywalled Gaussian splatting. The software can export files in a usable format for Unity(.obj, .fbx); this feature is also paywalled. This software is easy to use, has a mobile version, and does all model generation in the cloud. The cost is a $30 per month subscription for the features our project needs. 

### Option B: [RealityScan]

RealityScan is a free 3D scanning tool that lets users take photos, videos, and lidar scans and convert them into a 3D model; it has a mobile version that computes scans in the cloud and a local version that computes them locally. It is relatively easy to use and is also reasonably fast. The benefits are that it is completely free and doesn't require a subscription to export models in the format Unity needs; it is also fairly intuitive, offers more fine-tuned control over model settings, and makes it easier to tell when the pictures provide adequate coverage for a quality scan. The cost is that it will be hardware-intensive if we plan to compute scans locally. 

### Option C: Do Nothing

If we don't decide on a 3D scan tool (Do nothing), we will lose time we could've spent grabbing scans of the machine, processing the models, and bringing them into Unity. The only way delaying the decision makes sense is if we do not have access to the machine or if we can get away with using stock assets. 

## Analysis
Source for Polycam pricing: https://poly.cam/pricing 
Source for Realityscan tools: https://dev.epicgames.com/documentation/realityscan/model-tools 

The tradeoffs that matter are quality of scan, how easy it is to get usable video, and how long it takes to generate a scan. My evidence is from personal experience testing out the tools. I tested Polycam on an empty Ultra Paradise Monster can; it was relatively easy to capture enough frames with the mobile app, and it took 20-30 minutes to generate the object in the cloud. The quality was decent; I could read the lettering on the can, but the program clearly struggled with reflective areas, producing artifacts mainly around sharp edges like the can's lip. I then tested RealityScan; I was able to get a similar quality scan using video captured with my phone. The workflow was a little bit more time-consuming since I took the video, transferred it to my laptop, and converted .mov to .mp4 due to Windows codec throwing a fit and wanting $.99 I was unwilling to pay, and then I could import the video into the application. The longest part of the workflow of generating the scan was automatic image masking to separate the object from the environment, which took 20 minutes, and from there it only took a few seconds to generate the mesh and perform texture mapping. The quality was similar: I could read the lettering on the can, and it even seemed to pick up the can's dimpling. What I liked about this program was how much more control I had over the process, such as being able to simplify the mesh as I was running through the workflow instead of having to perform that step afterward; it also had the same issues with artefacting on sharp edges. I am uncertain how much better the object would be if I used lidar captures since we do have access to lidar cameras. 
Overall, based on my experimentation, both applications have promise. If the convenience of using Polycam is worth the subscription, it is a strong contender; RealityScan is also worth it because we can process and generate the object locally. I purposely chose an aluminum can for object testing because it's difficult to generate due to the material's reflectivity and the lip's unusual shape, which gives a good idea of what the applications can handle in the worst cases. 

## Recommendation and Verification Plan
Based on my testing and analysis, I recommend RealityScan. This is because we can do all model generation locally without relying on cloud computing;  it also allows exporting to .obj and .fbx without requiring a subscription. We will know it works when we can quickly generate models of test objects and machinery that require minimal manual correction, such as fixing a few minor defects, and that match the real object as closely as possible. Failure criteria are when the generated object has large defects that don’t match the real object or requires large amounts of manual correction. We will check this within the first 4 fully generated objects imported into Unity; if they don't meet quality standards upon import, we will know the workflow isn’t working and can re-evaluate before we invest too much time forcing a sub-par workflow to work. 

## Constraints

The only licensing constraint is that RealityScan is free only for education and companies making less than $ 1 million a year. No requirements on confidentiality for the machine, and no intellectual property issues. 

## Delegation and Validation

I did not use AI for the analysis portion; all the testing was performed manually. I used AI as a sanity check for the writing and to find sources for Polycam pricing and RealityScan tools. Verified the quality of the generated objects vs. the real object. 

## Decision

Proposed
