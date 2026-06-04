export default function NudgeHoldBlock({ wheelState, colStart }) {

    const classes = "gap-2 flex flex-col col-start" + colStart;

    return(
        <div className={classes}>
            <input onClick={() => wheelState.nudge()} className={wheelState.nudgeClasses} type="button" value="NUDGE"/>
            <input onClick={() => wheelState.toggleHold()} className={wheelState.holdClasses} type="button" value="HOLD"/>
        </div> 
    );
}