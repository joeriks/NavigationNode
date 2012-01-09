A file-only concept/sandbox super simple "CMS" using Razor syntax and some simple conventions
---

Use all Razor techniques for easy layouts, sections, partial page contents, helpers a.s.o
(Asp Net WebPages)

The only thing this "CMS" really does is a small NavigationNode class which can build a navigation node tree from the 
filesystem. Then you can use the navigation node tree to display html navigation with some simple Razor 
functions (sample top navigation and sub navigation included)

Folders and files with .cshtml extenstions and upper case first letters will show in the navigation (sets the VisibleInNavigation=true).

Folders and files with lower case first letters will get VisibleInNagivation=false (for example default.cshtml)

This sample runs from github (free account) https://github.com/joeriks/NavigationNode and autodeploy to AppHarbor (free account 1 instance) http://navigationnode.apphb.com/

Which basically means I can edit the razor files on GitHub, and on save they are committed and automatically deployed to the site, which is pretty cool.
