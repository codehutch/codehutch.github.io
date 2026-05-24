export default function Planet({ id, emoji, time, z, scale, orbit, year, dx, dy }) {
  
  let orbitN = parseFloat(orbit);

  let ddx = dx ? parseFloat(dx) : 0;
  let ddy = dy ? parseFloat(dy) : 0;

  let leftPos = ddx + 50.0 - scale * 2.5 + 1.75 * Math.cos(time * 500.0 / year) * orbitN / 2.0 * 50.0;  
  let topPos  = ddy + 50.0 - scale * 2.5 + 1.75 * Math.sin(time * 500.0 / year) * orbitN / 2.0 * 50.0;  

  let myStyle = {
    fontSize:scale + 'rem',
    position: 'absolute',
    left: leftPos + '%',
    top: topPos + '%',
    zIndex: z + ''
  };

  return (
    <span id={id} style={myStyle} className="relative"> 
        { emoji }
    </span>
  );

}