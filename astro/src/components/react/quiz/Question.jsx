import Answer from './Answer.jsx';
import { arrayToShuffled } from 'array-shuffle';

export default function Question({ children, answers }) {

  let augmentedAnswers = answers.map((x, i) => 
    ({ isCorrect: i == 0, text: x, key: i })
  );   

  let shuffledAnswers = augmentedAnswers; //arrayToShuffled(augmentedAnswers);

  return (
    <div className="Question">

      <h2>{children}</h2>
             
      {shuffledAnswers.map((x, i) => <Answer key={x.key} isCorrect={x.isCorrect} val={x.text}> {x.text} </Answer>)}

    </div>
  );
}