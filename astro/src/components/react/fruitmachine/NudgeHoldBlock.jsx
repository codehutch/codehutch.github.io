export default function NudgeHoldBlock({ wheelSet, colStart }) {

    const classes = "gap-2 flex flex-col col-start" + colStart;

    return(
        <div className={classes}>
            <input onClick={() => wheelSet.nudge()} className={wheelSet.nudgeClasses} type="button" value="NUDGE"/>
            <input onClick={() => wheelSet.toggleHold()} className={wheelSet.holdClasses} type="button" value="HOLD"/>
        </div> 
    );
}