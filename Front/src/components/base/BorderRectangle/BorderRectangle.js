import "./Check/BorderRectangle.css";

export default function BorderRectangle(props) {
  return (
    <div className="border-rectangle">
      {props.children}
    </div>
  );
}