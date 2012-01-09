This is a file-only CMS *)
---

Using Razor for easy layouts, sections, partial page contents and helpers
Traversing the file system on App Start to create navigation tree

@{
    var omitFolderNames = "App_Code,Bin,App_Data";
    App.Navigation = NavigationNode.PopulateFromFilePath(HttpContext.Current.Server.MapPath("/"),omitFolderNames);
    
}

Conventions:
.cshtml files with capital first letter will show in navigation
folders with capital first letter will show in navigation
This sample runs from github (free account) https://github.com/joeriks/NavigationNode and autodeploy to AppHarbor (free account 1 instance) http://navigationnode.apphb.com/

Which basically means I can edit the razor files on GitHub, and on save they are committed and automatically deployed to the site, which is pretty cool.

*) Just an experiment really, hold your horses... :-p