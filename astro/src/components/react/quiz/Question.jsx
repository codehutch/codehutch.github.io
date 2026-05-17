import Answer from './Answer.jsx';

export default function Question({ children, answers, kind }) {

  let augmentedAnswers = answers.map((x, i) => 
    ({ isCorrect: i == 0, text: x, key: i })
  );   

  let kindaHash = x => (x.text.length + x.text.charCodeAt(0)) ^ x.text.charCodeAt(x.text.length - 1); 

  let shuffledAnswers = augmentedAnswers.sort((a, b) => kindaHash(b) - kindaHash(a));

  return (
    <div className={kind}>

      <div>{children}</div>

      <div className="answerGroup">
        {shuffledAnswers.map((x, i) => <Answer key={x.key} isCorrect={x.isCorrect} val={x.text}> {x.text} </Answer>)}
      </div>

    </div>
  );
}