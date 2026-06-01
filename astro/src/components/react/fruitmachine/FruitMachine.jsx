import { useState, useEffect, useRef } from 'react';

import Reel from './Reel.jsx';
import NudgeHoldBlock from './NudgeHoldBlock.jsx';

class WheelState {

    static masterSpeedFactor = 0.08;
    static deltaSteps = 20;
    
    static initButtonClasses = "font-serif border-2 border-black rounded-md m-2 p-2";
    static initHoldClasses = WheelState.initButtonClasses + " bg-red-500";
    static initNudgeClasses = WheelState.initButtonClasses + " bg-orange-500";

    constructor(individualSpeedFactor, initialSpeed) {
        
        this.position = 0;
        this.targetPosition = 0;
        
        this.speed = 0;
        this.targetSpeed = 0;
        this.individualSpeedFactor = individualSpeedFactor;

        this.isSpeedSeek = true;

        this.isHold = false;
        this.canHold = false;
        this.holdClasses = WheelState.initHoldClasses;
        
        this.isNudge = false;
        this.canNudge = false;
        this.nudgeClasses = WheelState.initNudgeClasses;        

        this.setTargetSpeed(initialSpeed);
    }

    setPositionSeek(targetPosition) {
        this.isSpeedSeek = false;
        this.speed = 0;
        this.targetPosition = targetPosition;
    }

    advance() {
        if (this.isSpeedSeek)
            this.doSpeedSeek();
        else
            this.doPositionSeek();
    }

    doSpeedSeek() {

        let delta = (this.targetSpeed - this.speed) / WheelState.deltaSteps;

        this.speed += delta * WheelState.deltaSteps * 0.25;

        if (!this.isHold)
            this.position += this.speed;

        // Switch to position seek if nearly at 0 speed
        if (this.targetSpeed === 0 && this.speed < 0.001) {
            this.setPositionSeek(Math.round(this.position));
        }

    }

    doPositionSeek() {
        let delta = (this.targetPosition - this.position) / WheelState.deltaSteps;
        this.position += delta * 2;
    }

    setTargetSpeed(newTarget) {
        this.targetSpeed = newTarget * this.individualSpeedFactor * WheelState.masterSpeedFactor;
        this.isSpeedSeek = true;
    }

    toggleHold() {
      
      this.isHold = !this.isHold;
      this.holdClasses = this.getHoldClasses();

      if (this.isHold)
      {
        this.targetSpeed = 0;
        this.isSpeedSeek = true;
      }
    }

    getHoldClasses() {
      return WheelState.initHoldClasses +
             (this.isHold ? " animate-pulse" : "");
    }

    nudge() {
      
      //this.nudgeClasses = this.getNudgeClasses();

      this.targetSpeed = 0;
      this.setPositionSeek(Math.round(this.position + Math.round(Math.random() + 1)));
    }

    getNudgeClasses() {
      return WheelState.initNudgeClasses +
             (this.canNudge ? " animate-pulse" : "");
    }    
}


export default function FruitMachine() {

  const [time, setTime] = useState(0);
  
  const [ws0, setWS0] = useState(new WheelState(1.0, 1));
  const [ws1, setWS1] = useState(new WheelState(1.3, 1));
  const [ws2, setWS2] = useState(new WheelState(0.7, 1));

  const [spinDown, setSpinDown ] = useState(0);

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

  // Timer
  useInterval(() =>   { 

    setTime(time + 1 / 20) 

    if (spinDown > 0) {
        setSpinDown(spinDown - 1 / 20);
    }
    else if (spinDown < 0) {

        ws0.setTargetSpeed(0);
        ws1.setTargetSpeed(0);
        ws2.setTargetSpeed(0);

        setSpinDown(0);
    }

    ws0.advance();
    ws1.advance();
    ws2.advance();

  }, 50);

  function spin() {
    
    ws0.setTargetSpeed(10 + Math.random());
    ws1.setTargetSpeed(10 + Math.random());
    ws2.setTargetSpeed(10 + Math.random());

    setSpinDown(2.5);
  }

  return (

    <div className="container">
     <div className="rounded-md bg-cyan-500 border-2 border-black not-prose aspect-square max-w-[310px] mx-auto p-2 shadow-xl">

      <div className="aspect-square flex flex-row not-prose max-w-[290px] mx-auto">     
        <Reel wheelSet={ ws0 } /> 
        <Reel wheelSet={ ws1 } /> 
        <Reel wheelSet={ ws2 } />                 
      </div>

     </div>

     <div className="not-prose max-w-[310px] mx-auto p-2">

      <div className="grid grid-flow-col grid-rows-1 grid-cols-3 mx-auto not-prose max-w-[290px]">     
        <NudgeHoldBlock wheelSet={ws0} colStart="1" />
        <NudgeHoldBlock wheelSet={ws1} colStart="2" />
        <NudgeHoldBlock wheelSet={ws2} colStart="3" />
      </div>
      <div className="grid grid-flow-col grid-rows-1 grid-cols-3 mx-auto not-prose max-w-[290px]">     
        <div className="row-start-2 col-span-3 gap-2 flex flex-col">
            <input className="animate-pulse border-2 border-black bg-green-500 rounded-md m-2 p-2 font-serif text-2xl" type="button" onClick={spin} value="SPIN"/>
        </div> 
      </div>

     </div>
    
     {spinDown} 
     <br/>
     {ws0.position.toFixed(2)} - {ws0.targetPosition} <br/>
     {ws1.position.toFixed(2)} - {ws1.targetPosition} <br/> 
     {ws2.position.toFixed(2)} - {ws2.targetPosition} <br/>

      {ws2.holdClasses}
    </div>     

  );
}