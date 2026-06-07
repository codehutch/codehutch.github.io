import { Answer } from './Answer.tsx';

export interface QuestionProps {
  children: string;
  answers: string[];
  kind: string;
}

interface AnswerSummary {
  isCorrect: boolean;
  key: number;
  text: string;
}

export function Question({ children, answers, kind }: QuestionProps) {

  let augmentedAnswers: AnswerSummary[] = answers.map((x, i) => 
    ({ isCorrect: i == 0, text: x, key: i })
  );   

  let kindaHash = (x: AnswerSummary) => (x.text.length + x.text.charCodeAt(0)) ^ x.text.charCodeAt(x.text.length - 1); 

  let shuffledAnswers = augmentedAnswers.sort((a, b) => kindaHash(b) - kindaHash(a));

  return (
    <div className={kind}>

      <div>{children}</div>

      <div className="answerGroup">
        {shuffledAnswers.map((x, i) => <Answer key={x.key} isCorrect={x.isCorrect} val={x.text} />)}
      </div>

    </div>
  );
}