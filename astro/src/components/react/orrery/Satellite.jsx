export default function Planet({ id, emoji, time, z, scale, spin, orbit, year, fx, fy }) {
  
  let orbitN = parseFloat(orbit);

  let ffx = fx ? parseFloat(fx) : 1;
  let ffy = fy ? parseFloat(fy) : 1;

  let leftPos = 50.0 - scale * 2.5 + ffx * 1.75 * Math.cos(time * 500.0 / year) * orbitN / 2.0 * 50.0;  
  let topPos  = 50.0 - scale * 2.5 + ffy * 1.75 * Math.sin(time * 500.0 / year) * orbitN / 2.0 * 50.0;  

  let myStyle = {
    fontSize:scale + 'rem',
    position: 'absolute',
    left: leftPos + '%',
    top: topPos + '%',
    zIndex: z + '',
    transform: 'rotate(' + (( spin * time * 100 ) % 360) + 'deg)' 
  };

  return (
    <span id={id} style={myStyle} className="relative"> { emoji }</span>
  );

}