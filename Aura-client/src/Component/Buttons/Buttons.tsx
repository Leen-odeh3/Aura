import ButtonsProps from '../../Interfaces/ButtonsProps';

const Buttons = ({ img, text,onClick  }: ButtonsProps) => {
  return (
    <button style={{
      display: "flex", flexDirection: "row", padding: "12px 20px", borderRadius: "25px",
      backgroundColor: "white", cursor: "pointer", marginBottom: "15px", alignItems: "center",
      justifyContent: "center", border: "1px solid var(--secondary)",width:"270px"
    }} onClick={onClick}>
      {img && <img src={img} height="25px" width="25px"/>}
      <p style={{ fontWeight: "bold",margin:"0px 10px" }}>{text}</p>
    </button>



  )
}

export default Buttons
