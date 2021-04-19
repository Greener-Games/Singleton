#Usage
All singleton scripts are designed to be used within your own classes. 
Simply extend from the base class and your singleton logic will be available.

The class is set up to find the singleton object in a scene regardless if the Gameobject is on or not, if it can not find one it will create it.

If multiple singleton classes are loaded the last one to load will be destroyed.

Accessible though MyClass.Instance

##Unity Singleton
Scene singleton that will get cleaned up when the scene it is in gets destroyed.

```c#
Public class SceneSingleton : UnitySingleton<SceneSingleton>
{
    //your class here
}
```

##Unity singleton Persistent
A Persistant singleton that once spawned will not auto get cleaned up until after the game has unloaded

```c#
Public class PersistentSingleton : UnitySingletonPersistent<SceneSingleton>
{
    //your class here
}
```

##Singleton
A Version of the single that does not work from inheriting a script, Useful when you need to work within another inheritance sequence.
The main difference here is that its accessible though Accessible though MyClass.Instance.Get
```c#
Public class MyClass : SomeOtherClass
{
    public Singleton<MyClass> Instance = new Singleton<MyClass>();
    
    //your class here
}
```

#Methods

##Instance
Singleton way to access the Object

##ValidateInstance
Checks that the singleton exists and stores its variable for future access.
Option parameter to create the instance if it does not, this is defaulted to true.

##Exists
Checks if the singleton exists without creating it in the process.
