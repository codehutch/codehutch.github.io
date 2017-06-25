rm CurrentProject.fsproj
mv *.proj projectFiles/
mv *.fsproj projectFiles/
rm src/app.fsx
cp ../../BlogContent/blog/2017/06-22-fable-threejs-hello.fsx src/App.fsx
cp projectFiles/BlogFableThreeHelloWorldBuild.fsproj CurrentProject.fsproj
dotnet fable npm-run build
rm CurrentProject.fsproj
cp public/bundle.js ../../Code/content/otherOutput/fable1/BlogFableThreeHelloWorldBuild.js
mv public/bundle.js public/BlogFableThreeHelloWorldBuild.js
cp public/bundle.js.map ../../Code/content/otherOutput/fable1/BlogFableThreeHelloWorldBuild.js.map
mv public/bundle.js.map public/BlogFableThreeHelloWorldBuild.js.map