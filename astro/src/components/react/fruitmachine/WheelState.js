export default class WheelState {

    static masterSpeedFactor = 0.08;
    static deltaSteps = 20;
    
    static initButtonClasses = "font-serif border-2 border-black rounded-md m-2 p-2";

    constructor(individualSpeedFactor, initialSpeed) {
        
        this.position = 0;
        this.targetPosition = 0;
        
        this.speed = 0;
        this.targetSpeed = 0;
        this.individualSpeedFactor = individualSpeedFactor;

        this.isSpeedSeek = true;

        this.isHold = false;
        this.setCanHold(false);        
        this.setCanNudge(false);        

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

    setHold(hold) {
      this.isHold = hold;
      this.holdClasses = this.getHoldClasses();
    }

    setCanHold(canHold) {
      this.canHold = canHold;
      this.holdClasses = this.getHoldClasses();
    }

    toggleHold() {
      if (this.canHold) {
        this.setHold(!this.isHold);

        if (this.isHold) {
          this.targetSpeed = 0;
          this.isSpeedSeek = true;
        }
      }
    }

    getHoldClasses() {
      return WheelState.initButtonClasses +
             (this.canHold ? " bg-red-500 " : " bg-red-900 ") +
             (this.isHold ? " animate-pulse" : "");
    }

    nudge() {
      if (this.canNudge) {
        this.targetSpeed = 0;
        this.setPositionSeek(Math.round(this.position + Math.round(Math.random() + 1)));  
        this.setCanNudge(false);
      }
    } 

    setCanNudge(can) {
      this.canNudge = can;
      this.nudgeClasses = WheelState.initButtonClasses + 
                          (this.canNudge ? " bg-orange-500 " : " bg-orange-900 ");
    }

    isNearTarget() {
      return Math.abs(this.targetPosition - this.position) < 0.01;
    }
}