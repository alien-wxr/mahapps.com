///////////////////////////////////////////////////////////////////////////////
// TOOLS / ADDINS
///////////////////////////////////////////////////////////////////////////////

#tool nuget:?package=Wyam

#addin nuget:?package=Cake.Figlet
#addin nuget:?package=Cake.Wyam

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

///////////////////////////////////////////////////////////////////////////////
// PREPARATION
///////////////////////////////////////////////////////////////////////////////

var isLocal = BuildSystem.IsLocalBuild;

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
  Information(Figlet("MahApps.Metro Docs"));
});

Teardown(ctx =>
{
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Build")
    .Does(() =>
    {
      Wyam(new WyamSettings
      {
        Recipe = "Docs",
        Theme = "Samson",
        UpdatePackages = false,
        UseGlobalSources = false,
        UseLocalPackages = true
      });
    });
    
Task("Preview")
    .Does(() =>
    {
      Wyam(new WyamSettings
      {
        Recipe = "Docs",
        Theme = "Samson",
        UpdatePackages = true,
        UseGlobalSources = false,
        UseLocalPackages = true,
        Preview = true,
        Watch = true
      });
    });

///////////////////////////////////////////////////////////////////////////////
// TASK TARGETS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Preview")
    ;

Task("CIBuild")
    .IsDependentOn("Build")
    ;

///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);
