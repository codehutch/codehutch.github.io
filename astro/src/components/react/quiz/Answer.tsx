import { useState } from 'react';

export interface AnswerProps {
    isCorrect: boolean;
    val: string;
}

export function Answer({ isCorrect, val }: AnswerProps) {
    const [answerClass, setAnswerClass] = useState("answerUnknown");
    const check = () => setAnswerClass((x) => isCorrect ? "answerCorrect" : "answerIncorrect");   

    return (
        <button onMouseDown={check} onClick={check} className={answerClass}>{val}</button>
    );
}