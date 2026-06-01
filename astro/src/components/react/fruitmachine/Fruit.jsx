export default function Fruit({ icon, rotation }) {

  let scale = 3.8;

  let rotateMod = rotation % 360;

  let myStyle = {
    fontSize:scale + 'rem',
    position: 'absolute',
    backfaceVisibility: 'hidden',
    transform: 'translateY(88px) rotateX(' + rotateMod + 'deg) translateZ(135px)',
  };

  return (
    <span style={myStyle} className="relative aspect-square safari-text-shift-right-5">{icon}</span>
  );

}