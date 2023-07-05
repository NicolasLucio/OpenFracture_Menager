# Menager

## Disclaimer

Everything on this menager was made based on the limitations of the original script, so if you want something even more otimized, and I know that it's extremaly possible (ECS), you will not find it here. 

## The Menager Script

The base script:

![fragmentMenager](https://github.com/dgreenheck/OpenFracture/assets/54450123/30f578ff-8dd1-4e41-95b2-e6d349224e0d)

![fragmentMenager2](https://github.com/dgreenheck/OpenFracture/assets/54450123/1fcd02d6-7ccc-4411-abcf-c35bb211e5ad)


This is a simple script that gerenciate the 
explosion on scene, that means that which scene, if is necessary, has to have this script in some object.

That whay, you have the following variables:

| Variable Name      | Description |
| ----------- | ----------- |
| Check Timer      | This script is not checking with every tick, so here you define the time in <b>Seconds</b> of every time that the Menager will check if something was destructed. <br>Manipulate this variable is a wonderfull way to optimize your situation, so if your Scene is not destructing objects very often, you can let that number way more higher      |
| Destroy Timer  | After the Check return true, how much time in Seconds will take to the menager take the cleaning action.   |
| Destroy  | That will destroy all the fragments on the scene.<br>If <b>true</b>, it will hide the others options       |
| Destroy Fragments Rigidboy  | Remove the pyshics of the objects, so your character will not kick the fragments, and most important, will not calculate the objects physics anymore. |
| Remove Shadow | Remove the Mesh Rendering of the object, if you don't need the fragments shadows.<br>That really impact scenes with realtime lights.<br> |
| Show Debug Log Messages  | Just for Debug Porposses |

It is important to remember that even with de <b>"Destroy"</b> variable off, the fragment object will lose its take and will turn into a Static Object.

## Custom Editor

You can find the custom editor windows on:
>Window > Ksaa > Fragment Menager

![windowsEditor](https://github.com/dgreenheck/OpenFracture/assets/54450123/ccf86172-7352-4670-b5ac-4c5cd7ad059c)

The "Add Fragment Menager", will try to find if the Menager Script Object already exist on the Scene, if not, it will create one.

The "Add Explosion Objects" will add a Explosion Proxy Object.

![explosionGifwwww](https://github.com/dgreenheck/OpenFracture/assets/54450123/b7c60b54-7df9-4aab-92cb-59d36d4962cc)

## Fracture Script Diference

![fractureExplosion](https://github.com/dgreenheck/OpenFracture/assets/54450123/569523ff-2493-4fcd-a2f1-acd7cdb2c2ab)

The only diference between the original script, is on the "On Completed" Callback.
That will activate the Explosion Proxy Object.

## Explosion Script and Proxy Object

![explosionScript](https://github.com/dgreenheck/OpenFracture/assets/54450123/2714ddd9-082a-41cc-8069-ffc6dbe4fd10)

This script is needed on the Destroy Proxy Object, when activated, it will gerentiate the explosion process of the fragments arrount it.

That whay, you have the following variables:

| Variable Name      | Description |
| ----------- | ----------- |
| Explosion Force      | The force of the explosion      |
| Explosion Radius     | The radius of the explosion, this depend on the original object being exploded |
| Self Destroy         | Self destroy the Proxy Object<br>Destroy the proxy object in case you only want the explosion force.<br>If <b>true</b>, it will hide all the variables below. |
| Delay to Stop | Delay in seconds to stop the rigidbody of all the fragments. | 
| Range Multiplier     | This variable will multiply the "Explosion Radius" variable in ordem to catch the objects again to stop their rigidbodies components.<br>This is useful for situations that the "Delay to Stop" variable is set too high, and the fragments are to far away from its origin.<br>Be aware that this number can cause unwarted consequences, that could be even in other objetcts on the range or in your game performance. |
| Release              | If <b>true</b>, will release all the rigidbody that was stoped in the "Delay in Seconds" moment.<br>If <b>false</b>, it will hide the "Timer to Release" variable, and the fragment will stay stopped. |
| Timer to Release     | How much in time in seconds to release the rigidbody and calculate the fragments physics again. |
| Show Debug Log Messages | Just for Debug Porposses |


## Objects Name Tag

To work with the original script, some game objetcs have to have tags in they original names 

![gameObjectsTags](https://github.com/dgreenheck/OpenFracture/assets/54450123/3e266c13-70df-479b-a9cd-61d0cc798cac)

| Object Name  | Tag Syntax | Description |
| ----------- | ----------- | ----------- |
| gameObject      | _without tag_  |The object will act exacty the same as inputed in the Menager System Script |
| gameObject_i | _i | The object will be <b>ignored</b> by the Menager completely, in case you want the original and unchaged fragments on the scene | 
| gameObject_x | _x | The object will be treated with the <b>inverted</b> tags inputed on the Manager System Script<br>Ex. If you in the Menager, putted true to remove the shadow, the object with this tag will stay with the shadow. |
| gameObject_e | _e | For the Explosion System Script |