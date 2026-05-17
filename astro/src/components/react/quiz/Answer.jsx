import { useState } from 'react';

export default function Answer({ isCorrect, val }) {
    const [answerClass, setAnswerClass] = useState("answerUnknown");
    const check = () => setAnswerClass((x) => isCorrect ? "answerCorrect" : "answerIncorrect");   

    return (
        <button onMouseDown={check} onClick={check} className={answerClass}>{val}</button>
    );
}