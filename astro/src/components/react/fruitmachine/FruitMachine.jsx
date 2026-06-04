import { useState, useEffect, useRef } from 'react';

import Reel from './Reel.jsx';
import NudgeHoldBlock from './NudgeHoldBlock.jsx';
import WheelState from './WheelState.js';

export default function FruitMachine() {

  const [time, setTime] = useState(0);
  
  const [ws0, setWS0] = useState(new WheelState(1.0, 1));
  const [ws1, setWS1] = useState(new WheelState(1.3, 1));
  const [ws2, setWS2] = useState(new WheelState(0.7, 1));

  const [spinDown, setSpinDown] = useState(0); // Spin time count down
  
  const [winClass, setWinClass] = useState("container duration-900 transition ");

  // Timer
  useInterval(() =>   { 

    setTime(time + 1 / 20) 

    if (spinDown > 0) {
        setSpinDown(spinDown - 1 / 20);
    }
    else if (spinDown < 0) {

        allWheels(x => {
          x.setTargetSpeed(0);
          x.setCanNudge(true);
        });

        setSpinDown(0);
    }

    allWheels(x => x.advance());
    checkWin();

  }, 50);

  function spin() {
    
    allWheels(x => {
      x.setTargetSpeed(10 + Math.random());
      x.setCanNudge(false);
      x.setCanHold(true);
    });

    setSpinDown(2.0 + Math.random());
  }

  function checkWin() {
    if (ws0.speed < 0.001 && ws1.speed < 0.001 && ws2.speed < 0.001 &&
        ws0.isNearTarget() && ws1.isNearTarget() && ws2.isNearTarget() &&
        ws0.position.toFixed(2) % 12 === ws1.position.toFixed(2) % 12 &&
        ws0.position.toFixed(2) % 12 === ws2.position.toFixed(2) % 12) 
    {
      if (winClass.indexOf("rotate-360") > 0)
        setWinClass(winClass.replace("rotate-360", ""));
      else
        setWinClass(winClass + " rotate-360");

      confetti({ x: 0.3, y: 0.1, gravity: 0.7, startVelocity: 40, spread: 90, ticks: 200, zIndex: 1000 });
      confetti({ x: 0.7, y: 0.1, gravity: 0.9, startVelocity: 40, spread: 90, ticks: 200, zIndex: 1000 });
      confetti({ x: 0.4, y: 0.2, gravity: 0.8, startVelocity: 50, spread: 75, ticks: 200, zIndex: 1000 });
      confetti({ x: 0.6, y: 0.2, gravity: 0.7, startVelocity: 50, spread: 75, ticks: 200, zIndex: 1000 });
      confetti({ x: 0.3, y: 0.4, gravity: 0.7, startVelocity: 50, spread: 45, ticks: 200, zIndex: 1000 });
      confetti({ x: 0.7, y: 0.4, gravity: 0.9, startVelocity: 50, spread: 45, ticks: 200, zIndex: 1000 });

      ws0.speed = ws1.speed = ws2.speed = 1;

      allWheels(x => {
        x.setHold(false);
        x.setCanHold(false);
        x.setCanNudge(false);
      });
    } 
  }

  // Utility to act on all wheels
  function allWheels(fn) {
    fn(ws0);
    fn(ws1);
    fn(ws2);
  }

  return (
   <>
    <script src="https://cdn.jsdelivr.net/npm/canvas-confetti@1.4.0/dist/confetti.browser.min.js"></script>
    <div className={winClass}> 
      
      <div className="rounded-md bg-cyan-500 border-2 border-black not-prose aspect-square max-w-[310px] mx-auto p-2 shadow-xl">
        <div className="aspect-square flex flex-row not-prose max-w-[290px] mx-auto">     
          <Reel wheelState={ ws0 } /> 
          <Reel wheelState={ ws1 } /> 
          <Reel wheelState={ ws2 } />                 
        </div>
      </div>

      <div className="not-prose max-w-[310px] mx-auto p-2">
        <div className="grid grid-flow-col grid-rows-1 grid-cols-3 mx-auto not-prose max-w-[290px]">     
          <NudgeHoldBlock wheelState={ws0} colStart="1" />
          <NudgeHoldBlock wheelState={ws1} colStart="2" />
          <NudgeHoldBlock wheelState={ws2} colStart="3" />
        </div>
        <div className="grid grid-flow-col grid-rows-1 grid-cols-3 mx-auto not-prose max-w-[290px]">     
          <div className="row-start-2 col-span-3 gap-2 flex flex-col">
            <input className="animate-pulse ease-in-out border-2 border-black bg-green-500 text-2xl rounded-md m-2 p-2 font-serif" type="button" onClick={spin} value="SPIN"/>
          </div> 
        </div>
      </div>

    </div>     
   </>
  );

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

}