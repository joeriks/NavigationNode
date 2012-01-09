using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Linq;

/// <summary>
/// NavigationNode
/// </summary>
public class NavigationNode
{
    private string[] omitFolderNames;
    public int Id { get; set; }
    public bool VisibleInNavigation {get; set; }
    public string Name { get; set; }    
    public string Title { get; set; }
    public string Path { get; set; }
    public int Level { get; set; }
    public NavigationNode Parent {get; set;}
    public List<NavigationNode> Children { get; set; }
    public List<NavigationNode> VisibleChildren()
    {
        return Children.Where(n => n.VisibleInNavigation).ToList();
    }
    public NavigationNode(NavigationNode parent=null)
    {
        Parent = parent;
        Children = new List<NavigationNode>();
    }
    public NavigationNode Root()
    {
        return AncestorAtLevel(0);
    }
    public static NavigationNode PopulateFromFilePath(string path, string omitFileNamesCommaSeparated)
    {
        omitFileNames = omitFileNamesCommaSeparated.Split(',');
        var nodeTree = new NavigationNode(null);
        nodeTree.PopulateFileTree(path);
        return nodeTree;
    }
    public NavigationNode FindNodeByPath(string path)
    {
        return Descendants(n=>n.Path==path, true).SingleOrDefault();
    }
    public NavigationNode AncestorAtLevel(int level = 0)
    {
        if (this.Level < level) return null;

        var navigator = this;
        while (navigator.Parent != null && navigator.Level > level)
        {
            navigator = navigator.Parent;
        }
        return navigator;
    }
    public IEnumerable<NavigationNode> Ancestors()
    {
        var ancestors = new List<NavigationNode>();
        var navigator = this;
        while (navigator.Parent != null)
        {
            ancestors.Add(navigator.Parent);
        }
        return ancestors;
    }
    public IEnumerable<NavigationNode> Descendants(Func<NavigationNode, bool> func, bool deepSearch = false)
    {
        foreach (var child in this.Children)
        {
            if (func(child))
            {
                yield return child;

                foreach (var descendant in child.Descendants(func, deepSearch))
                {
                    yield return descendant;
                }
            }
            else if (deepSearch)
            {
                foreach (var descendant in child.Descendants(func, deepSearch))
                {
                    yield return descendant;
                }
            }
        }
    }
    public void PopulateFileTree(string filePath, NavigationNode node = null)
    {
        if (node == null) node = this;
        var directory = new DirectoryInfo(filePath);

        foreach (var subdirectory in directory.GetDirectories())
        {
            if (!OmitFoldersNames.Contains(subdirectory.Name))
            {
            var directoryNode = new NavigationNode(){
                 Name = subdirectory.Name,
                 VisibleInNavigation = subdirectory.Name.Substring(0, 1) == subdirectory.Name.Substring(0, 1).ToUpper(),
                 Path = node.Path + "/" + subdirectory.Name, 
                 Level = node.Level + 1,
                 Parent = node
            };
            PopulateFileTree(subdirectory.FullName, directoryNode);
            node.Children.Add(directoryNode);
            }
        }

        foreach (var file in directory.GetFiles())
        {
            if (file.Extension == ".cshtml" && !file.Name.StartsWith("_"))
            {
                var fileName = System.IO.Path.GetFileNameWithoutExtension(file.Name);
                var fileNode = new NavigationNode(){
                    Name = fileName,
                    VisibleInNavigation = fileName.Substring(0, 1) == fileName.Substring(0, 1).ToUpper(),
                    Path = node.Path + "/" + fileName,
                    Level = node.Level + 1,
                    Parent = node
                };
                node.Children.Add(fileNode);
            }
        }
    }

}
