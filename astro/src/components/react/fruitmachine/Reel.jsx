import Fruit from './Fruit.jsx';

export default function Reel({ position }) {

  let rotation = position * 360 / 12; // 12 fruits per wheel

  return (
    <div className="grow bg-white flex flex-col mr-2 ml-2 border-3 dark:border-black rounded-md shadow-3xl">
        <div className="relative flex flex-col flex-1 grow">
            <Fruit icon="🍒" rotation={rotation +   0}/>
            <Fruit icon="🔔" rotation={rotation +  30}/>
            <Fruit icon="🍫" rotation={rotation +  60}/>
            <Fruit icon="👑" rotation={rotation +  90}/>
            <Fruit icon="🍉" rotation={rotation + 120}/>
            <Fruit icon="🍐" rotation={rotation + 150}/>
            <Fruit icon="🫐" rotation={rotation + 180}/>
            <Fruit icon="🧭" rotation={rotation + 210}/>
            <Fruit icon="💝" rotation={rotation + 240}/>
            <Fruit icon="🍇" rotation={rotation + 270}/>
            <Fruit icon="🧲" rotation={rotation + 300}/>
            <Fruit icon="🍊" rotation={rotation + 330}/>  
            <div className="grow bg-gradient-to-t from-transparent via-transparent to-black relative"></div> 
            <div className="grow relative z-[999px] border-t-3 border-b-3 border-red-500/50 border-solid "></div>
            <div className="grow bg-gradient-to-b from-transparent to-black relative"></div>                  
        </div>
    </div>
  );

}