export default function Fruit({ icon, rotation }) {

  let rotateMod = rotation % 360;

  let fruitStyle = {
    fontSize:'3.8rem',
    position: 'absolute',
    backfaceVisibility: 'hidden',
    transform: 'translateY(88px) rotateX(' + rotateMod + 'deg) translateZ(135px)',
  };

  return (
    <span style={fruitStyle} className="relative aspect-square safari-text-shift-right-5">{icon}</span>
  );
}