rm CurrentProject.fsproj
mv *.proj projectFiles/
mv *.fsproj projectFiles/
rm src/app.fsx
cp ../../BlogContent/blog/2017/06-22-fable-threejs-hello.fsx src/App.fsx
cp projectFiles/BlogFableThreeHelloWorldBuild.fsproj CurrentProject.fsproj
dotnet fable npm-run build
rm CurrentProject.fsproj
mv public/bundle.js public/BlogFableThreeHelloWorldBuild.js
mv public/bundle.js.map public/BlogFableThreeHelloWorldBuild.js.map