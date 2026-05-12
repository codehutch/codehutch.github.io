echo ""
pwd
echo ""
echo "++++++++++++++++++++++++++++++++++++++++++"
echo "++ Please confirm current dir is ASTRO  ++"
echo "++++++++++++++++++++++++++++++++++++++++++"
echo ""
select yn in "Yes" "No"; do
    case $yn in
        Yes ) break;;
        No ) exit;;
    esac
done
echo "-------------------------------"
echo "--- pre-release git status: ---"
echo "-------------------------------"
echo ""
git status
echo ""
echo "== Total git status: =="
echo ""
git status | wc
echo ""
read -p "Press [Enter] key to continue..."
echo ""
echo ""
echo "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@"
echo "@@@@ Non-dist git status: @@@@"
echo "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@"
echo ""
git status | grep -iv dist | grep -v \\.\\.
echo ""
echo ""
echo ">>>>> Was the above git status CLEAN? <<<<<"
echo "!!!!! Commit non-dist files B4 deploy !!!!!"
echo "~~~  (don't commit the dist files yet)  ~~~"
echo ""
select yn in "Yes" "No"; do
    case $yn in
        Yes ) break;;
        No ) exit;;
    esac
done
echo ""
echo "????????????????????????????????????????????????"
echo "???? Is ALL really ready for release *TIME* ????"
echo "????????????????????????????????????????????????"
echo ""
select yn in "Yes" "No"; do
    case $yn in
        Yes ) break;;
        No ) exit;;
    esac
done
echo ""
echo ""
echo "###*^*### Has the non-release build been fully checked? ###*^*###"
echo ""
select yn in "Yes" "No"; do
    case $yn in
        Yes ) break;;
        No ) exit;;
    esac
done
echo ""
echo "OK then... running build..."
echo ""
npm run build
echo ""
echo ""
echo ">> CLEANing out ROOT<< " 
read -p "Press [Enter] key to continue..."
echo ""
rm -rf ../_astro
rm -rf ../blog
rm -rf ../articles
echo ""
echo ""
echo ">> Copying dist OVER root<< " 
read -p "Press [Enter] key to continue..."
echo ""
cp -r dist/* ..
rm -rf dist
echo ""
echo "-------------------------------"
echo "------ FINAL git status: ------"
echo "-------------------------------"
echo ""
git status
echo ""
echo ""
echo "-------------------------------"
echo "------ TO COMPLETE.. RUN: -----"
echo "---->  cd ..           <-------"
echo "------ git add            -----"
echo "------ git commit         -----"
echo "------ git push           -----"        
echo "-------------------------------"
echo ""
echo "(remember to cd.. AND >actually< do the above -----^^)"
echo "" 