rm CurrentProject.fsproj
mv *.proj projectFiles/
mv *.fsproj projectFiles/
cp projectFiles/BlogFableThreeMazeBuild.fsproj CurrentProject.fsproj
dotnet fable npm-run start
rm CurrentProject.fsproj
cp public/bundle.js ../../Code/content/otherOutput/fable1/BlogFableThreeMazeBuild.js
mv public/bundle.js public/BlogFableThreeMazeBuild.js
cp public/bundle.js.map ../../Code/content/otherOutput/fable1/BlogFableThreeMazeBuild.js.map
mv public/bundle.js.map public/BlogFableThreeMazeBuild.js.map