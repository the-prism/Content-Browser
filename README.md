# Content-Browser
C# dotnet core web app to browse content files

## Usage
The app is pretty straight forward. The Home page displays the list of folders and files in the content location.

In order to set the content location, you need to edit the Resources.resx file in the Properties folder and change the path to where you want to have your content root to be located.

```xml
<data name="ContentPath" xml:space="preserve">
  <value>C:/SomePath</value>
</data>
```
