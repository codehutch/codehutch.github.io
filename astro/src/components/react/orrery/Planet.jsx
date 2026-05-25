import { Children, cloneElement } from 'react';

export default function Planet({ id, emoji, dimensions, time, z, scale, orbit, year, dx, dy, children, parentX, parentY }) {
  
  let orbitN = parseFloat(orbit);

  let ppx = parentX ? parseFloat(parentX) : 0;
  let ppy = parentY ? parseFloat(parentY) : 0;

  let ddx = dx ? parseFloat(dx) : 0;
  let ddy = dy ? parseFloat(dy) : 0;

  let leftPos = ppx + ddx + 50.0 - scale * 2.5 + 1.75 * Math.cos(time * 500.0 / year) * orbitN / 2.0 * 50.0;  
  let topPos  = ppy + ddy + 50.0 - scale * 2.3 + 1.75 * Math.sin(time * 500.0 / year) * orbitN / 2.0 * 50.0;  

  let scaleFactor = scale * (dimensions ? (parseFloat(dimensions.width) / 300) : 1.0);

  let myStyle = {
    fontSize:scaleFactor + 'rem',
    position: 'absolute',
    left: leftPos + '%',
    top: topPos + '%',
    zIndex: z + '',
    transform: 'rotate(' + ((Math.cos(time * 0.2 * scale) * 50 ) % 360) + 'deg)'     
  };

  return (
    <><span id={id} style={myStyle} className="relative">{emoji}</span>{Children.map(children, (child) => cloneElement(child, { parentX: leftPos, parentY: topPos }))}</>
  );

}