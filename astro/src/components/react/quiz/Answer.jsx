import { useState } from 'react';

export default function Answer({ children, isCorrect, val }) {
    const [answerClass, setAnswerClass] = useState("answerUnknown");
    const check = () => setAnswerClass((x) => isCorrect ? "answerCorrect" : "answerIncorrect");    

    return (
        <button onClick={check} className={answerClass}>{val}</button>
    );
}
