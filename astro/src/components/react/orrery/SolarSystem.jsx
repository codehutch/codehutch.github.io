import { useState, useEffect, useRef } from 'react';

import Planet from './Planet.jsx';
import Satellite from './Satellite.jsx';

export default function SolarSystem() {

  const [speed, setSpeed] = useState(1.0);  
  const [time, setTime] = useState(200);
  const [dimensions, setDimensions] = useState({ width: 0, height: 0 });
  const mainDiv = useRef(null);

  // Timer
  useInterval(() => {
    setTime(time + speed / 50);    
  }, 50);

  // Resize
  useEffect(() => {
    if (mainDiv.current) {
      const observer = new ResizeObserver((entries) => {
        for (let entry of entries) {
          setDimensions({
            width: entry.contentRect.width,
            height: entry.contentRect.height,
          });
        }       
      });

      observer.observe(mainDiv.current);

      return () => {
        observer.disconnect();
      };
    }    
  }, []);

  // Twinkly stars
  let twinkle = (factor) => {

    let amount = Math.abs(Math.round(time * 10 * factor)) % 8;

    if (amount == 1 || amount == 3)
      return "scale(0.7)";
    else
      return "scale(1.0)";
  }

  // Speed button handlers
  const normal = () =>  setSpeed(1.0)
  const fast = () =>    setSpeed(3.0)
  const reverse = () => setSpeed(-1.0)

  // UseInterval hook from https://overreacted.io
  function useInterval(callback, delay) {
    const savedCallback = useRef();
  
    // Remember the latest callback.
    useEffect(() => {
      savedCallback.current = callback;
    }, [callback]);
  
    // Set up the interval.
    useEffect(() => {
      function tick() {
        savedCallback.current();
      }
      if (delay !== null) {
        let id = setInterval(tick, delay);
        return () => clearInterval(id);
      }
    }, [delay]);
  }

  return (

  <>
    <div className="prose dark:hidden">Looks best in <strong>dark mode</strong> <div className="inline-block text-2xl animate-bounce">☝️</div> (click the sun in the title bar)</div>

    <div 
      id="solarSystem" 
      ref={mainDiv}
      className="relative aspect-square"
      style={{background: "repeating-radial-gradient(rgba(128,0,128,0), rgba(128,0,128,0) 8%, rgba(128,255,128,1) 9%, rgba(128,0,128,0) 10%)"}}
    > 

        <div className="absolute" style={{right:10+"%", top:10+"%", transform:twinkle(0.5)}}>🌟</div>
        <div className="absolute" style={{right:7+"%", bottom:51+"%"}}>⭐</div> 
        <div className="absolute" style={{right:20+"%", bottom:35+"%"}}>🌟</div>      

        <div className="absolute transition" style={{left:12+"%", bottom:15+"%", transform:twinkle(1.3)}}>🌟</div>
        <div className="absolute" style={{left:2+"%", top:9+"%"}}>⭐</div> 
        <div className="absolute" style={{left:30+"%", top:23+"%"}}>✨</div> 

        <div className="absolute transition" style={{right:15+"%", top:28+"%", transform:twinkle(2.2)}}>✨</div>
        <div className="absolute" style={{right:19+"%", bottom:11+"%"}}>⭐</div> 
        <div className="absolute" style={{right:27+"%", bottom:41+"%"}}>✨</div>      

        <div className="absolute" style={{left:33+"%", bottom:21+"%"}}>✨🌟</div>
        <div className="absolute" style={{left:27+"%", top:11+"%", transform:twinkle(1.7)}}>⭐</div> 
        <div className="absolute" style={{left:5+"%", top:9+"%"}}>✨</div>         

        <Planet id="sun"     emoji="🌞" time={time} z="210" scale="3"   orbit="0.0"  year="0.01" dimensions={dimensions} dx="-2.5" dy="-7.3"/>
        <Planet id="mercury" emoji="⚪" time={time} z="230" scale="0.8" orbit="0.3"  year="88"  />
        <Planet id="venus"   emoji="🟤" time={time} z="240" scale="1"   orbit="0.36" year="225" />
        <Planet id="earth"   emoji="🌍" time={time} z="250" scale="1.8" orbit="0.45" year="365">
          <Planet id="moon"  emoji="🌝" time={time} z="255" scale="1"   orbit="0.15" year="60" dx="-45" dy="-45"/>
        </Planet>
        <Planet id="mars"    emoji="🔴" time={time} z="220" scale="1.4" orbit="0.59" year="687" />
        <Planet id="jupiter" emoji="🟠" time={time} z="260" scale="2"   orbit="0.75" year="4333"/>
        <Planet id="saturn"  emoji="🪐" time={time} z="270" scale="3.3" orbit="0.90" year="6789"/>
        <Planet id="uranus"  emoji="🪩" time={time} z="290" scale="1.5" orbit="1.05" year="8888"/>
        <Planet id="neptune" emoji="🔵" time={time} z="280" scale="1.3" orbit="1.15" year="9999"/>

        <Satellite id="sputnik" emoji="🛰️" time={time} z="110" scale="3"   orbit="0.9" year="1100" spin="-1.0" fx="-1"   fy="2"    />
        <Satellite id="rocket"  emoji="🚀" time={time} z="330" scale="2"   orbit="0.7" year="1200" spin="-1.5" fx="1"    fy="-2"   />
        <Satellite id="saucer"  emoji="🛸" time={time} z="140" scale="2"   orbit="0.5" year="1300" spin="-0.5" fx="-0.5" fy="-1.5" />
        <Satellite id="frisbee" emoji="🥏" time={time} z="350" scale="1.5" orbit="0.3" year="1400" spin="-2.5" fx="0.5"  fy="1.5"  />
        <Satellite id="invader" emoji="👾" time={time} z="160" scale="2"   orbit="0.4" year="1450" spin="1.3" fx="-0.7" fy="-1.7" />
        <Satellite id="comet"   emoji="☄️" time={time} z="390" scale="2"   orbit="0.6" year="1350" spin="0.7" fx="-0.9" fy="-1.85"/>
        <Satellite id="astro"   emoji="👩‍🚀" time={time} z="180" scale="1.5" orbit="0.8" year="1250" spin="1.7" fx="0.9"  fy="1.85" />
        <Satellite id="alien"   emoji="👽" time={time} z="370" scale="1.5" orbit="1.0" year="1150" spin="2.1" fx="0.6"  fy="1.6"  />
   
    </div>

    <div id="graphicsContainer" className="answerGroup">
      <div className="buttonContainer">
        <button onClick={normal}>Normal</button>
        <button onClick={fast}>Fast</button>
        <button onClick={reverse}>Reverse</button>
      </div>
    </div>
  </> 

  );
  
}