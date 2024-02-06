# Deploiement Asp net core sur Azure
- creer un compte sur azure et prendre un abonnement (la version gratuite est trop limitée)
- lier son compte azure à visual studio
- lier son profil github à Azure et VisualStudio
- Aller dans la Market Place Azure et choisir la ressource webapp + database - configurer tout ca
  ![image](https://github.com/GrincoManoir/MyJobBoard/assets/85107357/79da634a-6dc0-47f8-b902-0cd5013452f6)

- Dans visual studio, l'assistant de publication est le meilleur moyen d'obtenir un profil de publication viable
- le profil de publication est alors générée en associant la publication aux ressources souscrites dans azure
  ![image](https://github.com/GrincoManoir/MyJobBoard/assets/85107357/698c4a1c-393e-4f63-b787-3dfa1011fc20)
- la partie gestion des api peut être ignoré si aucune ressource "gestion des api" n'a été souscrite sur azure
- on peut alors choisir de deployer l'application directement sur la cible ( la ressource de type App Service de notre groupe de ressource )
  soit réaliser la publication par l'intermédiare d'un workflow github
  ![image](https://github.com/GrincoManoir/MyJobBoard/assets/85107357/ab52f049-1708-4e6e-b10a-832e50ea69af)
- attention les workflows gitub ne permette de faire les publication qu'en utilisant la méthode "ZipDeploy" tandis qu'Azure propose d'autre méthode ( FTP et WebDeploy)
- Pour fonctionner la publication "ZipDeploy" il faut creer la variable d'environnement au niveau de la webApp (AppService) -> configuration -> Nouveau paramètre d'application 
 WEBSITE_RUN_FROM_PACKAGE avec la valeur 1. Sans ca l'application est en echec au démarrage.
![image](https://github.com/GrincoManoir/MyJobBoard/assets/85107357/ec9b3ad5-9f26-4be5-8155-5c1d28e55bd6)
- Pour pouvoir fonctionner le workflow demande à pouvoir stocker les secret fourni par azure (essentiellement le profil de publication fourni par azure qui contient les informations permettant l'acces au ressources azure)
- Pour pouvoir stocker ces secret il faut créer une organisation dans github et à default d' upgrade son compte github le repos doit être public pour avoir le droit d'utiliser la fonctionnalité des secrets.
- Lorsque la branche contenant le workflow est pusher , le workflow s'execute et l'application est déployé sur la web app
- Pour pouvoir déclencher manuellement le workflow dans github action ajouter cette ligne  workflow_dispatch: dans le fichier yml
- voici le fichier yml du worflow :
  
```
- name: Générer et déployer une application .NET Core sur l'application web myjobboardapi
on:
  push:
    branches:
    - master
  workflow_dispatch:
  
env:
  AZURE_WEBAPP_NAME: myjobboardapi
  AZURE_WEBAPP_PACKAGE_PATH: MyJobBoard.Web.Host/publish
  CONFIGURATION: Release
  DOTNET_CORE_VERSION: 8.0.x
  WORKING_DIRECTORY: MyJobBoard.Web.Host
jobs:
  build:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v4
    - name: Setup .NET SDK
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: ${{ env.DOTNET_CORE_VERSION }}
    - name: Restore
      run: dotnet restore "${{ env.WORKING_DIRECTORY }}"
    - name: Build
      run: dotnet build "${{ env.WORKING_DIRECTORY }}" --configuration ${{ env.CONFIGURATION }} --no-restore
    - name: Test
      run: dotnet test "${{ env.WORKING_DIRECTORY }}" --no-build
    - name: Publish
      run: dotnet publish "${{ env.WORKING_DIRECTORY }}" --configuration ${{ env.CONFIGURATION }} --no-build --output "${{ env.AZURE_WEBAPP_PACKAGE_PATH }}"
    - name: Publish Artifacts
      uses: actions/upload-artifact@v3
      with:
        name: webapp
        path: ${{ env.AZURE_WEBAPP_PACKAGE_PATH }}
  deploy:
    runs-on: ubuntu-latest
    needs: build
    steps:
    - name: Download artifact from build job
      uses: actions/download-artifact@v3
      with:
        name: webapp
        path: ${{ env.AZURE_WEBAPP_PACKAGE_PATH }}
    - name: Deploy to Azure WebApp
      uses: azure/webapps-deploy@v2
      with:
        app-name: ${{ env.AZURE_WEBAPP_NAME }}
        publish-profile: ${{ secrets.myjobboardapi_E0BA }}
        package: ${{ env.AZURE_WEBAPP_PACKAGE_PATH }}
        ```
- en autorisant l'UI de swagger en production (Program.cs) on peut constater que l'application est bien déployer en se rendant à l'url {maWebAppUrl}/swagger/index.html
