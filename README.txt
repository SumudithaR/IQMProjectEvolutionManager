Whats in this project.
===========================================
This project contains a client and address domain objects.
These are used purely to demonstrate the BaseController and AjaxController.

Feel free to remove these objects.

The BaseController
-------------------------------------------
Contains all the actions for Creating, Editing and Searching domain objects.
If anything more complex is required implement this in the specific controller.

This controller relies on the shared views Create, Edit, Search in the shared dir, which in turn loads in
the CreateForm, EditForm, SearchForm and SearchResults in the specific domain controller view directory.

You will also find other sub views in there (Detail, ExtendedDetail, RowHeader, RowHeaderExt, RowDetail, RowDetailExt.
These later are used for displaying the results of searches.


The AjaxController
-------------------------------------------
The ajax controller inheirits from the Base controller and includes the actions for searching for domain items via ajax calls,
it also handles adding of items to other domain objects via ajax and returning results that get loaded into the DOM.

This controller is tightly linked to the ajax-utils.js file and the three views in the AjaxTemplates

The controller expects to have two types of views templates defined.
These are a display template for the domain object and an Editor Template for the Domain object view model.


Converting this project to one of your own.
===========================================
The Default project is called "ProjectName".

1. Rename the projectname.build file. Open it and replace all instances of ProjectName with the name of your project.
2. Rename the ProjectName.sln file.
3. Open the project.
4. Rename the projects, and change the names of the assemblies in the project properties.
5. Refactor the ProjectName namespace to be the one you want.
6. Close the project.
7. Rename the directory structures ProjectName, ProjectName.Core and ProjectName.Test.
8. Open the solution file in a text editor. Replace ProjectName with your project name.
9. Open the Project.
10. Unload the main project. Edit the csproj file and change the IIS Deploy value to that of your project.
11. Open the .bzrignore file and Replace ProjectName with your project name.