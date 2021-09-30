import "./BookingTables.css";

export default function BookingTables(props) {
  const bookATable = props.bookATable;
  const unbookATable = props.unbookATable;
  const bookedTablesData = props.bookedTablesData;
  /*
  bookedTablesData = {
      tableId: uuid,
      isBooked: bool,
      isBookedByClient: bool,
      bookedTableSeconds: double
    }
  */
  return (
    //Sorry
    <svg className="booking-tables__cafe-layout"
      xmlns="http://www.w3.org/2000/svg"
      width="401.02mm"
      height="360.67mm"
      viewBox="0 0 401.02 360.67"
    >
      <g transform="translate(633.11 -201.71)">
        <g fill="none" stroke="#d2d2d2">
          <g strokeLinecap="round" strokeLinejoin="round" strokeWidth="0.7">
            <path d="M-593.18 393.78h-23.476M-593.18 367.17h-23.476M-603.36 393.78v168.24h346.66v-94.687h-9.39v-255.11h-327.1v341.97h327.1v-86.861M-256.69 445.42v-55.56h23.476v-10.955h-22.693v-22.693h10.173M-266.08 356.21h10.173M-245.74 334.3h-20.346"></path>
            <path d="M-255.91 334.3V212.22h23.476v-10.173h-370.92v165.11M-428.85 554.2v7.825M-418.68 554.2v7.825M-363.9 554.2v7.825M-337.29 554.2v7.825M-281.73 554.2v7.825M-266.08 202.06v10.173M-321.64 202.06v10.173M-331.82 202.06v10.173M-386.59 202.06v10.173M-396.77 202.06v10.173M-451.54 202.06v10.173M-461.72 202.06v10.173M-517.28 202.06v10.173M-526.67 202.06v10.173M-582.23 202.06v10.173M-484.41 554.2v7.825M-245.74 445.42a10.955 10.955 0 01-10.955 10.955m10.955 10.955a10.955 10.955 0 00-10.955-10.955m-9.39-10.955h20.346m-20.346 21.911h20.346M-245.74 334.3a10.955 10.955 0 01-10.955 10.955m10.955 10.955a10.955 10.955 0 00-10.955-10.955m-9.39-10.955h20.346m-20.346 21.911h20.346M-616.66 367.17v4.043a9.26 9.26 45 009.26 9.26h4.826"></path>
            <path d="M-616.66 393.78v-4.043a9.26 9.26 135 019.26-9.26h4.826"></path>
          </g>
          <g strokeWidth="1.6">
            <path d="M-525.93 212.23v36.779"></path>
            <path strokeLinecap="square" d="M-329.19 213.02v33.5"></path>
            <path
              strokeLinejoin="round"
              d="M-328.69 291.26v38.344h62.603"
            ></path>
          </g>
          <path
            strokeLinecap="round"
            strokeLinejoin="round"
            strokeWidth="1.6"
            d="M-266.93 360.13h-52.404a8.996 8.996 135 00-8.996 8.996v10.638"
          ></path>
          <g strokeWidth="1.6">
            <path
              strokeLinecap="round"
              d="M-266.61 441.51h-53.082a8.996 8.996 45 01-8.996-8.996V422.11"
            ></path>
            <path d="M-369.38 329.61h10.173v102.11a9.79 9.79 135 01-9.79 9.79h-112.29M-593.18 329.61h68.08v-38.344"></path>
          </g>
          <g strokeLinecap="round" strokeLinejoin="round" strokeWidth="0.7">
            <path d="M-524.61 292.02v-7.88a20.406 20.29 0 0120.406-20.29h10.287"></path>
            <path d="M-518.96 292.02l.029-2.396c.133-11.246 9.285-20.293 20.532-20.298h1.35M-525.22 292.02h6.26M-497.05 263.85v5.478"></path>
            <path d="M-513.48 296.72l.025-2.04a20.891 20.891 0 0120.536-20.63l1.35-.023M-524.43 296.72h10.955M-491.57 263.85v10.173M-329.15 292.02v-7.88a20.61 20.29 0 00-20.61-20.29h-10.39"></path>
            <path d="M-334.28 292.02l-.029-2.396c-.133-11.246-9.285-20.293-20.531-20.298h-1.35M-328.43 292.02h-5.844M-356.19 263.85v5.478"></path>
            <path d="M-339.76 296.72l-.025-2.04a20.891 20.891 0 00-20.536-20.63l-1.35-.023M-328.8 296.72h-10.955M-361.67 263.85v10.173M-493.92 263.85h134.6"></path>
          </g>
          <path strokeWidth="1.6" d="M-603.36 406.3h-28.954V301.64"></path>
          <g strokeWidth="0.7">
            <path d="M-631.27 302.22v55.56h27.841M-631.27 352.3h27.918M-631.27 346.82h27.918M-631.23 341.03h27.872M-631.41 335.37h28.055M-631.36 329.75h27.888M-631.86 324.05h28.643M-631.36 318.7h27.87M-631.61 313h28.331M-631.36 307.16h28.15M-631.53 301.98h28.139M-593.18 351.52l51.536-.678a8.043 8.043 134.62 007.937-8.042v-12.408"></path>
            <path d="M-524.32 329.61v19.13a12.171 12.171 135 01-12.171 12.171h-56.692M-593.18 402.72h51.647"></path>
          </g>
          <g strokeLinecap="round" strokeWidth="1.6">
            <path d="M-541.54 403.17h6.897a10.319 10.319 45 0110.319 10.319v28.808M-502.28 498.5a93.774 93.774 0 008.465 8.791l.8.738M-524.32 442.29a88.669 93.367 0 004.464 25.262M-493.02 508.03v45.387"></path>
          </g>
          <path
            strokeDasharray="1.2, 1.2"
            strokeLinecap="round"
            strokeLinejoin="round"
            strokeWidth="0.3"
            d="M-524.6 443.08l.091 2.005a88.006 88.006 64.868 0028.646 61.065l2.064 1.88"
          ></path>
          <path
            strokeDasharray="1.2, 1.2"
            strokeLinecap="round"
            strokeLinejoin="round"
            strokeWidth="0.3"
            d="M-523.86 443.06l.155 2.454a88.743 88.743 64.758 0027.899 59.177l3.566 3.34"
          ></path>
          <g>
            <g strokeWidth="1.6">
              <path d="M-593.18 521.33l19.285-1.866a190.34 190.34 165.7 0056.252-14.337l15.237-6.49"></path>
              <path
                strokeLinecap="round"
                d="M-592.7 494.03l14.695-2.316a127.4 127.4 159.88 0046.311-16.962l11.841-7.193"
              ></path>
              <path d="M-593.18 441.51h68.863"></path>
            </g>
            <g strokeLinecap="round" strokeWidth="0.7">
              <path
                strokeLinejoin="round"
                d="M-553.59 402.72v6.874l11.738-6.186M-425.72 362.47v-39.127h-63.385v64.168h63.385"
              ></path>
              <path d="M-438.24 374.21l.307.077a7.026 7.026 36.536 014.32 3.201l.85 1.417M-438.24 359.34l.76 1.014a5.292 5.292 26.565 004.233 2.116h7.528"></path>
            </g>
          </g>
        </g>
        <g
          fill="#d2d2d2"
          stroke="#d2d2d2"
          strokeLinecap="round"
          strokeLinejoin="round"
          strokeWidth="0.7"
        >
          <path
            d="M-434.33 378.93H-425.722V387.538H-434.33z"
            opacity="0.999"
          ></path>
          <path
            d="M-434.33 323.35H-425.722V331.958H-434.33z"
            opacity="0.999"
          ></path>
          <path
            d="M-489.11 323.35H-480.502V331.958H-489.11z"
            opacity="0.999"
          ></path>
          <path
            d="M-489.11 378.91H-480.502V387.51800000000003H-489.11z"
            opacity="0.999"
          ></path>
          <path
            d="M-377.2 322.57H-369.375V330.395H-377.2z"
            opacity="0.999"
          ></path>
          <path
            d="M-377.2 379.69H-369.375V387.515H-377.2z"
            opacity="0.999"
          ></path>
        </g>
        <path
          fill="none"
          stroke="#d2d2d2"
          strokeWidth="1.6"
          d="M-359.21 380.47h-10.173"
        ></path>
        <g
          fill="#d2d2d2"
          stroke="#d2d2d2"
          strokeLinecap="round"
          strokeLinejoin="round"
          strokeWidth="0.7"
        >
          <path
            d="M-378.77 433.69H-370.945V441.515H-378.77z"
            opacity="0.999"
          ></path>
          <path
            d="M-433.55 433.69H-425.725V441.515H-433.55z"
            opacity="0.999"
          ></path>
          <path
            d="M-489.11 434.47H-481.285V442.295H-489.11z"
            opacity="0.999"
          ></path>
        </g>
        <g fill="none" stroke="#d2d2d2" strokeWidth="0.7">
          <path
            strokeDasharray="1.4, 1.4"
            strokeLinecap="round"
            d="M-432.76 362.47v16.433M-425.72 362.47v16.433"
          ></path>
          <path d="M-438.24 359.34v-16.608a6.085 6.085 45 00-6.085-6.085h-25.391a6.085 6.085 135 00-6.086 6.085v25.391a6.085 6.085 45 006.086 6.085h31.476"></path>
          <path d="M-432.76 378.91h-43.214a6.085 6.085 45 01-6.085-6.085V337.26a6.085 6.085 135 016.085-6.086h37.129a6.085 6.085 45 016.085 6.086v25.216"></path>
          <g strokeLinecap="round" strokeLinejoin="round">
            <circle cx="-475.89" cy="316.26" r="3.917" opacity="0.999"></circle>
            <circle cx="-463.05" cy="316.08" r="3.917" opacity="0.999"></circle>
            <circle cx="-450.3" cy="316.26" r="3.917" opacity="0.999"></circle>
            <circle cx="-438.52" cy="316.36" r="3.917" opacity="0.999"></circle>
          </g>
        </g>
        <g
          fill="none"
          stroke="#d2d2d2"
          strokeLinecap="round"
          strokeLinejoin="round"
          strokeWidth="0.447"
          transform="matrix(0 1.5651 -1.5651 0 4.603 314.13)"
        >
          <circle cx="13.879" cy="320.58" r="2.503" opacity="0.999"></circle>
          <circle cx="22.085" cy="320.46" r="2.503" opacity="0.999"></circle>
          <circle cx="30.23" cy="320.58" r="2.503" opacity="0.999"></circle>
          <circle cx="37.755" cy="320.64" r="2.503" opacity="0.999"></circle>
        </g>
        <g
          fill="none"
          stroke="#d2d2d2"
          strokeLinecap="round"
          strokeLinejoin="round"
          strokeWidth="0.447"
          transform="rotate(180 -208.28 448.2) scale(1.5651)"
        >
          <circle cx="13.879" cy="320.58" r="2.503" opacity="0.999"></circle>
          <circle cx="22.085" cy="320.46" r="2.503" opacity="0.999"></circle>
          <circle cx="30.23" cy="320.58" r="2.503" opacity="0.999"></circle>
          <circle cx="37.755" cy="320.64" r="2.503" opacity="0.999"></circle>
        </g>
        <g fill="none" stroke="#d2d2d2" strokeLinejoin="round">
          <g strokeLinecap="round" strokeWidth="0.7" transform="rotate(-90)">
            <circle
              cx="-358.83"
              cy="-418.26"
              r="3.917"
              opacity="0.999"
            ></circle>
            <circle
              cx="-346.08"
              cy="-418.08"
              r="3.917"
              opacity="0.999"
            ></circle>
            <circle cx="-334.3" cy="-417.99" r="3.917" opacity="0.999"></circle>
          </g>
          <g strokeWidth="0.3">
            <path d="M-442.94 443.86h24.259m-24.259-1.565v1.565h-2.348v1.565h28.954v-1.565h-2.347v-1.565M-356.86 389.86v21.128m-1.565-21.128h1.565v-2.348h1.565v25.824h-1.565v-2.347h-1.565M-267.65 259.18v21.911m1.565-21.911h-1.565v-2.348h-1.565v26.606h1.565v-2.347h1.565M-331.03 302.22v21.911m1.565-21.911h-1.565v-2.348h-1.565v26.606h1.565v-2.347h1.565M-522.75 324.13v-21.128m-1.565 0h1.565v-2.348h1.565v25.824h-1.565v-2.347h-1.565"></path>
          </g>
          <path
            strokeWidth="0.4"
            d="M-586.73 339.04h10.173m-.783 7.825v.783h1.566v-8.608h-.783v-1.565h-10.173v1.565h-.782v8.608h1.565v-.783m0-7.042v7.042h8.608v-7.042zM-550.66 339.04h10.173m-.783 7.825v.783h1.566v-8.608h-.783v-1.565h-10.173v1.565h-.782v8.608h1.565v-.783m0-7.042v7.042h8.608v-7.042z"
          ></path>
        </g>
        <g fill="#d2d2d2" strokeWidth="0.265" fontFamily="Calibri">
          <text
            x="-457.408"
            y="359.075"
            fontSize="11.994"
            style={{
              lineHeight: "3.15",
              shapeInside: "url(#path2081)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-465.652" y="358.54">
              <tspan>Бар</tspan>
            </tspan>
          </text>
          <text
            x="-560.89"
            y="375.713"
            fontSize="16.564"
            style={{
              lineHeight: "0.65",
              shapeInside: "url(#path10208)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-571.1" y="339.579">
              <tspan fontSize="6.625">Касса</tspan>
            </tspan>
          </text>
          <text
            x="-563.05"
            y="469.474"
            fontSize="16.564"
            transform="translate(0 10.054)"
            style={{
              lineHeight: "1.25",
              shapeInside: "url(#rect10216)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-564.987" y="456.548">
              <tspan>Ж</tspan>
            </tspan>
          </text>
        </g>
        <g fill="none">
          <path d="M-583.45 235.7H-572.8340000000001V246.31599999999997H-583.45z"></path>
          <g stroke="#d2d2d2">
            <path
              strokeWidth="1.6"
              d="M-326.353 475.667a48.947 48.947 0 0118.448-3.61h41.552M-356.856 554.051V521a48.947 48.947 0 013.61-18.448"
            ></path>
            <path
              strokeDasharray="0.9, 0.9"
              strokeLinecap="round"
              strokeWidth="0.3"
              d="M-357.36 554.06v-33.552a48.948 48.948 135 0148.948-48.948h42.052"
            ></path>
            <path
              strokeDasharray="0.9, 0.9"
              strokeLinecap="round"
              strokeWidth="0.3"
              d="M-356.36 554.06v-32.552a48.948 48.948 135 0148.948-48.948h41.052"
            ></path>
          </g>
        </g>
        <g id="tableN10" fill="none" stroke="#d2d2d2"
          className={"booking-tables__table" + (
            bookedTablesData[10]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[10]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[10]?.isBooked ?
            () => bookATable(bookedTablesData[10]?.tableId) :
            bookedTablesData[10]?.isBooked &&
              bookedTablesData[10]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[10]?.tableId) :
              null}>
          <g strokeWidth="0.713">
            <path
              strokeLinejoin="round"
              d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
              transform="rotate(-90 -154.86 335.08) scale(.98228)"
            ></path>
            <path
              d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
              transform="rotate(-90 -154.86 335.08) scale(.98228)"
            ></path>
          </g>
          <path
            strokeLinejoin="round"
            strokeWidth="0.7"
            d="M-433.89 268.97v15.717h15.717V268.97z"
          ></path>
          <g strokeWidth="0.713">
            <path
              strokeLinejoin="round"
              d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
              transform="rotate(90 -367.785 5.655) scale(.98228)"
            ></path>
            <path
              d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
              transform="rotate(90 -367.785 5.655) scale(.98228)"
            ></path>
          </g>
          <text
            x="-453.088"
            y="322.864"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="10.583"
            style={{
              lineHeight: "0",
              shapeInside: "url(#path3300-74-8)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-431.792" y="275.85">
              <tspan fontSize="5.644" style={{ lineHeight: "3" }}>
                №10
              </tspan>
            </tspan>
            <tspan x="-431.792" y="278.85" dy="3">
              <tspan fontSize="5.644" style={{ lineHeight: "3" }}>
                {(bookedTablesData[10]?.bookedTableSeconds != 0) ?
                  bookedTablesData[10]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g id="tableN1" className={"booking-tables__table" + (
          bookedTablesData[1]?.isBooked ?
            " booking-tables__table_is-booked" : "") + (
            bookedTablesData[1]?.isBookedByClient ?
              " booking-tables__table_is-booked-by-client" :
              "")}
          onClick={!bookedTablesData[1]?.isBooked ?
            () => bookATable(bookedTablesData[1]?.tableId) :
            bookedTablesData[1]?.isBooked &&
              bookedTablesData[1]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[1]?.tableId) :
              null}>
          <path
            fill="inherit"
            stroke="#d2d2d2"
            strokeLinejoin="round"
            strokeWidth="0.7"
            d="M-588.86 292.94v16.798h25.447V292.94z"
          ></path>
          <g fill="inherit" stroke="#d2d2d2" strokeWidth="0.829">
            <path
              strokeLinejoin="round"
              d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
              transform="matrix(.84824 0 0 .83988 -492.71 245.02)"
            ></path>
            <path
              d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
              transform="matrix(.84824 0 0 .83988 -492.71 245.02)"
            ></path>
          </g>
          <g fill="inherit" stroke="#d2d2d2">
            <path
              strokeLinejoin="round"
              strokeWidth="0.829"
              d="M-278.36 70.055v18h40v-18z"
              transform="matrix(.84824 0 0 .83988 -356.99 255.1)"
            ></path>
            <path
              strokeWidth="0.415"
              d="M-278.36 85.055h40"
              transform="matrix(.84824 0 0 .83988 -356.99 255.1)"
            ></path>
            <path
              strokeWidth="0.829"
              d="M-258.36 70.055v18"
              transform="matrix(.84824 0 0 .83988 -356.99 255.1)"
            ></path>
          </g>
          <text
            x="-584.145"
            y="306.421"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="8.467"
            wordSpacing="0"
            style={{
              lineHeight: "2",
              shapeInside: "url(#path1887-9-5-5-6)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-582.63" y="300.523">
              <tspan>№1</tspan>
            </tspan>
            <tspan x="-582.63" y="307.523">
              <tspan>
                {(bookedTablesData[1]?.bookedTableSeconds != 0) ?
                  bookedTablesData[1]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g id="tableN2"
          className={"booking-tables__table" + (
            bookedTablesData[2]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[2]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[2]?.isBooked ?
            () => bookATable(bookedTablesData[2]?.tableId) :
            bookedTablesData[2]?.isBooked &&
              bookedTablesData[2]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[2]?.tableId) :
              null}>

          <g fill="inherit" stroke="#d2d2d2" strokeWidth="0.833">
            <path
              strokeLinejoin="round"
              d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
              transform="matrix(.8415 0 0 .83988 -459.46 245.02)"
            ></path>
            <path
              d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
              transform="matrix(.8415 0 0 .83988 -459.46 245.02)"
            ></path>
          </g>
          <g fill="inherit" stroke="#d2d2d2">
            <path
              strokeLinejoin="round"
              strokeWidth="0.833"
              d="M-278.36 70.055v18h40v-18z"
              transform="matrix(.8415 0 0 .83988 -324.82 255.1)"
            ></path>
            <path
              strokeWidth="0.416"
              d="M-278.36 85.055h40"
              transform="matrix(.8415 0 0 .83988 -324.82 255.1)"
            ></path>
            <path
              strokeWidth="0.833"
              d="M-258.36 70.055v18"
              transform="matrix(.8415 0 0 .83988 -324.82 255.1)"
            ></path>
          </g>
          <path
            fill="inherit"
            stroke="#d2d2d2"
            strokeLinejoin="round"
            strokeWidth="0.7"
            d="M-554.85 292.94v16.798h25.245V292.94z"
          ></path>

          <text
            x="-541.797"
            y="305.429"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="8.467"
            style={{
              lineHeight: "2",
              shapeInside: "url(#path1887-9-5-5-6-8)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-548.714" y="300.523">
              <tspan>№2</tspan>
            </tspan>
            <tspan x="-548.714" y="307.523">
              <tspan>
                {(bookedTablesData[2]?.bookedTableSeconds != 0) ?
                  bookedTablesData[2]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g id="tableN3"
          className={"booking-tables__table" + (
            bookedTablesData[3]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[3]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[3]?.isBooked ?
            () => bookATable(bookedTablesData[3]?.tableId) :
            bookedTablesData[3]?.isBooked &&
              bookedTablesData[3]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[3]?.tableId) :
              null}>
          <g fill="inherit" stroke="#d2d2d2">
            <path
              strokeLinejoin="round"
              strokeWidth="0.7"
              d="M-563.69 248.3v-16.735h-25.252V248.3z"
            ></path>
            <g strokeWidth="0.834">
              <path
                strokeLinejoin="round"
                d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                transform="matrix(-.84172 0 0 -.83676 -659.1 296.04)"
              ></path>
              <path
                d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                transform="matrix(-.84172 0 0 -.83676 -659.1 296.04)"
              ></path>
            </g>
            <path
              strokeLinejoin="round"
              strokeWidth="0.834"
              d="M-278.36 70.055v18h40v-18z"
              transform="matrix(-.84172 0 0 -.83676 -793.78 286)"
            ></path>
            <path
              strokeWidth="0.417"
              d="M-278.36 85.055h40"
              transform="matrix(-.84172 0 0 -.83676 -793.78 286)"
            ></path>
            <path
              strokeWidth="0.834"
              d="M-258.36 70.055v18"
              transform="matrix(-.84172 0 0 -.83676 -793.78 286)"
            ></path>
          </g>
          <text
            x="-573.552"
            y="237.055"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="8.467"
            style={{
              lineHeight: "2",
              shapeInside: "url(#path1887-9-5-5-6-0)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-582.804" y="239.15">
              <tspan>№3</tspan>
            </tspan>
            <tspan x="-582.804" y="246.15">
              <tspan>
                {(bookedTablesData[3]?.bookedTableSeconds != 0) ?
                  bookedTablesData[3]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g id="tableN4"
          className={"booking-tables__table" + (
            bookedTablesData[4]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[4]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[4]?.isBooked ?
            () => bookATable(bookedTablesData[4]?.tableId) :
            bookedTablesData[4]?.isBooked &&
              bookedTablesData[4]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[4]?.tableId) :
              null}>
          <path
            fill="inherit"
            stroke="#d2d2d2"
            strokeLinejoin="round"
            strokeWidth="0.7"
            d="M-529.9 248.31v-16.722h-25.28v16.722z"
          ></path>
          <g fill="inherit" stroke="#d2d2d2" strokeWidth="0.834">
            <path
              strokeLinejoin="round"
              d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
              transform="matrix(-.84267 0 0 -.83611 -625.42 296.02)"
            ></path>
            <path
              d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
              transform="matrix(-.84267 0 0 -.83611 -625.42 296.02)"
            ></path>
          </g>
          <g fill="inherit" stroke="#d2d2d2">
            <path
              strokeLinejoin="round"
              strokeWidth="0.834"
              d="M-278.36 70.055v18h40v-18z"
              transform="matrix(-.84267 0 0 -.83611 -760.25 285.98)"
            ></path>
            <path
              strokeWidth="0.417"
              d="M-278.36 85.055h40"
              transform="matrix(-.84267 0 0 -.83611 -760.25 285.98)"
            ></path>
            <path
              strokeWidth="0.834"
              d="M-258.36 70.055v18"
              transform="matrix(-.84267 0 0 -.83611 -760.25 285.98)"
            ></path>
          </g>
          <text
            x="-536.621"
            y="241.452"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="8.467"
            style={{
              lineHeight: "2",
              shapeInside: "url(#path1887-9-5-5-6-8-8)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-549.03" y="239.175">
              <tspan>№4</tspan>
            </tspan>
            <tspan x="-549.03" y="246.175">
              <tspan>
                {(bookedTablesData[4]?.bookedTableSeconds != 0) ?
                  bookedTablesData[4]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <text
          x="-488.356"
          y="197.055"
          fill="#d2d2d2"
          strokeWidth="0.265"
          fontFamily="Calibri"
          fontSize="10.583"
          style={{ lineHeight: "1.25" }}
        ></text>
        <g id="tableN5"
          className={"booking-tables__table" + (
            bookedTablesData[5]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[5]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[5]?.isBooked ?
            () => bookATable(bookedTablesData[5]?.tableId) :
            bookedTablesData[5]?.isBooked &&
              bookedTablesData[5]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[5]?.tableId) :
              null}>
          <path
            fill="inherit"
            stroke="#d2d2d2"
            strokeLinejoin="round"
            strokeWidth="0.7"
            d="M-500.94 212.35v32h16.075v-32z"
          ></path>
          <g fill="inherit" stroke="#d2d2d2" strokeWidth="0.676">
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M-151.36 12.055h18v32h-18v-32"
              transform="matrix(1.0717 0 0 1 -363.39 200.3)"
            ></path>
            <path
              d="M-151.36 15.055h18M-146.36 15.055v26M-151.36 41.055h18M-146.36 28.055h13"
              transform="matrix(1.0717 0 0 1 -363.39 200.3)"
            ></path>
          </g>
          <g fill="inherit" stroke="#d2d2d2" strokeWidth="0.676">
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M-151.36 12.055h18v32h-18v-32"
              transform="matrix(-1.0717 0 0 -1 -622.42 256.41)"
            ></path>
            <path
              d="M-151.36 15.055h18M-146.36 15.055v26M-151.36 41.055h18M-146.36 28.055h13"
              transform="matrix(-1.0717 0 0 -1 -622.42 256.41)"
            ></path>
          </g>
          <text
            x="-487.238"
            y="233.932"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="10.583"
            style={{
              lineHeight: "3.05",
              shapeInside: "url(#path2269-2)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-499.394" y="228.137">
              <tspan fontSize="8.467">№5</tspan>
            </tspan>
            <tspan x="-499.394" y="236.137">
              <tspan fontSize="8">
                {(bookedTablesData[5]?.bookedTableSeconds != 0) ?
                  bookedTablesData[5]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g id="tableN6"
          className={"booking-tables__table" + (
            bookedTablesData[6]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[6]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[6]?.isBooked ?
            () => bookATable(bookedTablesData[6]?.tableId) :
            bookedTablesData[6]?.isBooked &&
              bookedTablesData[6]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[6]?.tableId) :
              null}>
          <path
            fill="inherit"
            stroke="#d2d2d2"
            strokeLinejoin="round"
            strokeWidth="0.7"
            d="M-435.32 212.35v32h16.075v-32z"
          ></path>
          <g fill="inherit" stroke="#d2d2d2" strokeWidth="0.676">
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M-151.36 12.055h18v32h-18v-32"
              transform="matrix(1.0717 0 0 1 -297.77 200.3)"
            ></path>
            <path
              d="M-151.36 15.055h18M-146.36 15.055v26M-151.36 41.055h18M-146.36 28.055h13"
              transform="matrix(1.0717 0 0 1 -297.77 200.3)"
            ></path>
          </g>
          <g fill="inherit" stroke="#d2d2d2" strokeWidth="0.676">
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M-151.36 12.055h18v32h-18v-32"
              transform="matrix(-1.0717 0 0 -1 -556.8 256.41)"
            ></path>
            <path
              d="M-151.36 15.055h18M-146.36 15.055v26M-151.36 41.055h18M-146.36 28.055h13"
              transform="matrix(-1.0717 0 0 -1 -556.8 256.41)"
            ></path>
          </g>
          <text
            x="-436.346"
            y="252.055"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="10.583"
            style={{
              lineHeight: "3.05",
              shapeInside: "url(#path2269-2-01)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-433.778" y="228.137">
              <tspan fontSize="8.467">№6</tspan>
            </tspan>
            <tspan x="-433.778" y="236.137">
              <tspan fontSize="8">
                {(bookedTablesData[6]?.bookedTableSeconds != 0) ?
                  bookedTablesData[6]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g id="tableN7"
          className={"booking-tables__table" + (
            bookedTablesData[7]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[7]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[7]?.isBooked ?
            () => bookATable(bookedTablesData[7]?.tableId) :
            bookedTablesData[7]?.isBooked &&
              bookedTablesData[7]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[7]?.tableId) :
              null}>
          <path
            fill="inherit"
            stroke="#d2d2d2"
            strokeLinejoin="round"
            strokeWidth="0.7"
            d="M-370.16 212.35v32h15.916v-32z"
          ></path>
          <g fill="inherit" stroke="#d2d2d2" strokeWidth="0.68">
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M-151.36 12.055h18v32h-18v-32"
              transform="matrix(1.0611 0 0 1 -233.96 200.3)"
            ></path>
            <path
              d="M-151.36 15.055h18M-146.36 15.055v26M-151.36 41.055h18M-146.36 28.055h13"
              transform="matrix(1.0611 0 0 1 -233.96 200.3)"
            ></path>
          </g>
          <g fill="inherit" stroke="#d2d2d2" strokeWidth="0.68">
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M-151.36 12.055h18v32h-18v-32"
              transform="matrix(-1.0611 0 0 -1 -490.44 256.41)"
            ></path>
            <path
              d="M-151.36 15.055h18M-146.36 15.055v26M-151.36 41.055h18M-146.36 28.055h13"
              transform="matrix(-1.0611 0 0 -1 -490.44 256.41)"
            ></path>
          </g>
          <text
            x="-371.383"
            y="257.055"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="10.583"
            style={{
              lineHeight: "3.05",
              shapeInside: "url(#path2269-2-9)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-368.689" y="228.137">
              <tspan fontSize="8.467">№7</tspan>
            </tspan>
            <tspan x="-368.689" y="236.137">
              <tspan fontSize="8.467">
                {(bookedTablesData[7]?.bookedTableSeconds != 0) ?
                  bookedTablesData[7]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>

        <g id="tableN9"
          className={"booking-tables__table" + (
            bookedTablesData[9]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[9]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[9]?.isBooked ?
            () => bookATable(bookedTablesData[9]?.tableId) :
            bookedTablesData[9]?.isBooked &&
              bookedTablesData[9]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[9]?.tableId) :
              null}>
          <g fill="inherit" stroke="#d2d2d2">
            <path
              strokeLinejoin="round"
              strokeWidth="0.7"
              d="M-423.89 293.97v15.717h15.717V293.97z"
              transform="rotate(-30 -481.68 422.11)"
            ></path>
            <g strokeWidth="0.713">
              <path
                strokeLinejoin="round"
                d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                transform="rotate(-30 -481.68 422.11) rotate(-90 -137.36 342.58) scale(.98228)"
              ></path>
              <path
                d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                transform="rotate(-30 -481.68 422.11) rotate(-90 -137.36 342.58) scale(.98228)"
              ></path>
            </g>
            <g strokeWidth="0.713">
              <path
                strokeLinejoin="round"
                d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                transform="rotate(-30 -481.68 422.11) rotate(90 -375.285 23.155) scale(.98228)"
              ></path>
              <path
                d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                transform="rotate(-30 -481.68 422.11) rotate(90 -375.285 23.155) scale(.98228)"
              ></path>
            </g>
          </g>
          <text
            x="-454.754"
            y="297.055"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="5.644"
            style={{
              lineHeight: "3",
              shapeInside: "url(#path3300-74-8-6)",
              whiteSpace: "pre",
            }}
            transform="rotate(-30 -481.68 422.11)"
          >
            <tspan x="-420.359" y="300.85">
              <tspan>№9</tspan>
            </tspan>
            <tspan x="-420.359" y="306.85">
              <tspan>
                {(bookedTablesData[9]?.bookedTableSeconds != 0) ?
                  bookedTablesData[9]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>

          </text>
        </g>
        <g id="tableN11"
          className={"booking-tables__table" + (
            bookedTablesData[11]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[11]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[11]?.isBooked ?
            () => bookATable(bookedTablesData[11]?.tableId) :
            bookedTablesData[11]?.isBooked &&
              bookedTablesData[11]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[11]?.tableId) :
              null}>
          <g fill="inherit" stroke="#d2d2d2">
            <g strokeWidth="0.713">
              <path
                strokeLinejoin="round"
                d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                transform="rotate(30 -363.05 373.89) rotate(-90 -136.27 338.85) scale(.98228)"
              ></path>
              <path
                d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                transform="rotate(30 -363.05 373.89) rotate(-90 -136.27 338.85) scale(.98228)"
              ></path>
            </g>
            <path
              strokeLinejoin="round"
              strokeWidth="0.7"
              d="M-419.07 291.34v15.717h15.717V291.34z"
              transform="rotate(30 -363.05 373.89)"
            ></path>
            <g strokeWidth="0.713">
              <path
                strokeLinejoin="round"
                d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                transform="rotate(30 -363.05 373.89) rotate(90 -371.56 24.25) scale(.98228)"
              ></path>
              <path
                d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                transform="rotate(30 -363.05 373.89) rotate(90 -371.56 24.25) scale(.98228)"
              ></path>
            </g>
          </g>
          <text
            x="-373.528"
            y="307.055"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="5.644"
            style={{
              lineHeight: "3",
              shapeInside: "url(#path3300-74-8-8)",
              whiteSpace: "pre",
            }}
            transform="rotate(30 -363.05 373.89)"
          >
            <tspan x="-416.974" y="298.216">
              <tspan>№11</tspan>
            </tspan>
            <tspan x="-416.974" y="304.216">
              <tspan>
                {(bookedTablesData[11]?.bookedTableSeconds != 0) ?
                  bookedTablesData[11]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>

          </text>
        </g>
        <g id="tableN12"
          className={"booking-tables__table" + (
            bookedTablesData[12]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[12]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[12]?.isBooked ?
            () => bookATable(bookedTablesData[12]?.tableId) :
            bookedTablesData[12]?.isBooked &&
              bookedTablesData[12]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[12]?.tableId) :
              null}>
          <path
            fill="inherit"
            stroke="#d2d2d2"
            strokeLinejoin="round"
            strokeWidth="0.7"
            d="M-398.26 337.06v35h16v-35z"
          ></path>
          <g fill="inherit" stroke="#d2d2d2" strokeWidth="0.671">
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M-404.36 212.06h-18v-45h18v45"
              transform="matrix(1 0 0 1.0881 45.096 148.72)"
            ></path>
            <path
              d="M-409.36 196.06h-13M-409.36 209.06v-39M-404.36 170.06h-18M-409.36 183.06h-13M-404.36 209.06h-18"
              transform="matrix(1 0 0 1.0881 45.096 148.72)"
            ></path>
            <g>
              <path
                strokeLinejoin="round"
                d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                transform="matrix(1 0 0 1.0881 -291.9 277.93)"
              ></path>
              <path
                d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                transform="matrix(1 0 0 1.0881 -291.9 277.93)"
              ></path>
            </g>
            <g>
              <path
                strokeLinejoin="round"
                d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                transform="matrix(-1 0 0 -1.0881 -488.62 431.17)"
              ></path>
              <path
                d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                transform="matrix(-1 0 0 -1.0881 -488.62 431.17)"
              ></path>
            </g>
          </g>
          <text
            x="-391.916"
            y="312.055"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="10.583"
            style={{
              lineHeight: "3.15",
              shapeInside: "url(#path2269-2-0)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-396.02" y="352.369">
              <tspan fontSize="5.644">№12</tspan>
            </tspan>
            <tspan x="-396.02" y="360.369">
              <tspan fontSize="5.644">
                {(bookedTablesData[12]?.bookedTableSeconds != 0) ?
                  bookedTablesData[12]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>

          </text>
        </g>
        <g id="tableN13"
          className={"booking-tables__table" + (
            bookedTablesData[13]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[13]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[13]?.isBooked ?
            () => bookATable(bookedTablesData[13]?.tableId) :
            bookedTablesData[13]?.isBooked &&
              bookedTablesData[13]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[13]?.tableId) :
              null}>
          <g fill="inherit" stroke="#d2d2d2">
            <g strokeWidth="0.744">
              <path
                strokeLinejoin="round"
                d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                transform="translate(-280.12 351.86) scale(.94139)"
              ></path>
              <path
                d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                transform="translate(-280.12 351.86) scale(.94139)"
              ></path>
            </g>
            <g strokeWidth="0.744">
              <path
                strokeLinejoin="round"
                d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                transform="rotate(180 -232.65 234.68) scale(.94139)"
              ></path>
              <path
                d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                transform="rotate(180 -232.65 234.68) scale(.94139)"
              ></path>
            </g>
            <circle
              cx="410.64"
              cy="372.78"
              r="7.531"
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth="0.7"
              transform="rotate(90)"
            ></circle>
          </g>
          <text
            x="-384.185"
            y="405.049"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="4.939"
            style={{
              lineHeight: "1.05",
              shapeInside: "url(#path3580-6)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-377.819" y="410.127">
              <tspan>№13</tspan>
            </tspan>
            <tspan x="-377.819" y="415.127">
              <tspan>
                {(bookedTablesData[13]?.bookedTableSeconds != 0) ?
                  bookedTablesData[13]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g id="tableN14"
          className={"booking-tables__table" + (
            bookedTablesData[14]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[14]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[14]?.isBooked ?
            () => bookATable(bookedTablesData[14]?.tableId) :
            bookedTablesData[14]?.isBooked &&
              bookedTablesData[14]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[14]?.tableId) :
              null}>
          <g fill="inherit" stroke="#d2d2d2">
            <g strokeWidth="0.744">
              <path
                strokeLinejoin="round"
                d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                transform="rotate(90 -434.47 79.88) scale(.94139)"
              ></path>
              <path
                d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                transform="rotate(90 -434.47 79.88) scale(.94139)"
              ></path>
            </g>
            <g strokeWidth="0.744">
              <path
                strokeLinejoin="round"
                d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                transform="rotate(-90 -71.46 400.63) scale(.94139)"
              ></path>
              <path
                d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                transform="rotate(-90 -71.46 400.63) scale(.94139)"
              ></path>
            </g>
            <circle
              cx="413.38"
              cy="-421.7"
              r="7.531"
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth="0.7"
              transform="scale(-1)"
            ></circle>
          </g>
          <text
            x="-410.853"
            y="409.555"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="10.583"
            style={{
              lineHeight: "0",
              shapeInside: "url(#path3580-6-0)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-418.417" y="421.178">
              <tspan fontSize="4.939" style={{ lineHeight: "1.05" }}>
                №14
              </tspan>
            </tspan>
            <tspan x="-418.417" y="426.178">
              <tspan fontSize="4.939" style={{ lineHeight: "1.05" }}>
                {(bookedTablesData[14]?.bookedTableSeconds != 0) ?
                  bookedTablesData[14]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g id="tableN15"
          className={"booking-tables__table" + (
            bookedTablesData[15]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[15]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[15]?.isBooked ?
            () => bookATable(bookedTablesData[15]?.tableId) :
            bookedTablesData[15]?.isBooked &&
              bookedTablesData[15]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[15]?.tableId) :
              null}>
          <g fill="inherit" stroke="#d2d2d2" transform="translate(-51.927 -.176)">
            <g strokeWidth="0.744">
              <path
                strokeLinejoin="round"
                d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                transform="rotate(90 -434.47 79.88) scale(.94139)"
              ></path>
              <path
                d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                transform="rotate(90 -434.47 79.88) scale(.94139)"
              ></path>
            </g>
            <g strokeWidth="0.744">
              <path
                strokeLinejoin="round"
                d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                transform="rotate(-90 -71.46 400.63) scale(.94139)"
              ></path>
              <path
                d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                transform="rotate(-90 -71.46 400.63) scale(.94139)"
              ></path>
            </g>
            <circle
              cx="413.38"
              cy="-421.7"
              r="7.531"
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth="0.7"
              transform="scale(-1)"
            ></circle>
          </g>
          <text
            x="-410.927"
            y="409.555"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="10.583"
            style={{
              lineHeight: "0",
              shapeInside: "url(#path3580-6-0-2)",
              whiteSpace: "pre",
            }}
            transform="translate(-51.927 -.176)"
          >
            <tspan x="-418.417" y="421.178">
              <tspan fontSize="4.939" style={{ lineHeight: "1.05" }}>
                №15
              </tspan>
            </tspan>
            <tspan x="-418.417" y="426.178">
              <tspan fontSize="4.939" style={{ lineHeight: "1.05" }}>
                {(bookedTablesData[15]?.bookedTableSeconds != 0) ?
                  bookedTablesData[15]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g id="tableN16"
          className={"booking-tables__table" + (
            bookedTablesData[16]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[16]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[16]?.isBooked ?
            () => bookATable(bookedTablesData[16]?.tableId) :
            bookedTablesData[16]?.isBooked &&
              bookedTablesData[16]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[16]?.tableId) :
              null}>
          <g fill="inherit" stroke="#d2d2d2">
            <g strokeWidth="0.992">
              <path
                strokeLinejoin="round"
                d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                transform="rotate(-45 -471.2 466.54) rotate(180 -270.38 257.91) scale(.70584)"
              ></path>
              <path
                d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                transform="rotate(-45 -471.2 466.54) rotate(180 -270.38 257.91) scale(.70584)"
              ></path>
            </g>
            <path
              strokeLinejoin="round"
              strokeWidth="0.7"
              d="M-480.51 475.55h18.352v-18.352h-18.352z"
              transform="rotate(-45 -471.2 466.54)"
            ></path>
            <g strokeWidth="0.992">
              <path
                strokeLinejoin="round"
                d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                transform="rotate(-45 -471.2 466.54) translate(-401.91 416.92) scale(.70584)"
              ></path>
              <path
                d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                transform="rotate(-45 -471.2 466.54) translate(-401.91 416.92) scale(.70584)"
              ></path>
              <g>
                <path
                  strokeLinejoin="round"
                  d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                  transform="rotate(-45 -471.2 466.54) rotate(90 -478.835 56.955) scale(.70584)"
                ></path>
                <path
                  d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                  transform="rotate(-45 -471.2 466.54) rotate(90 -478.835 56.955) scale(.70584)"
                ></path>
              </g>
              <g>
                <path
                  strokeLinejoin="round"
                  d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                  transform="rotate(-45 -471.2 466.54) rotate(-90 -61.915 458.865) scale(.70584)"
                ></path>
                <path
                  d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                  transform="rotate(-45 -471.2 466.54) rotate(-90 -61.915 458.865) scale(.70584)"
                ></path>
              </g>
            </g>
          </g>
          <text
            x="-508.404"
            y="443.056"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="5.644"
            style={{
              lineHeight: "3.45",
              shapeInside: "url(#path3300-2)",
              whiteSpace: "pre",
            }}
            transform="rotate(-45 -471.2 466.54)"
          >
            <tspan x="-477.092" y="465.341">
              <tspan>№16</tspan>
            </tspan>
            <tspan x="-477.092" y="471.341">
              <tspan>
                {(bookedTablesData[16]?.bookedTableSeconds != 0) ?
                  bookedTablesData[16]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g id="tableN17"
          className={"booking-tables__table" + (
            bookedTablesData[17]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[17]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[17]?.isBooked ?
            () => bookATable(bookedTablesData[17]?.tableId) :
            bookedTablesData[17]?.isBooked &&
              bookedTablesData[17]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[17]?.tableId) :
              null}>
          <g fill="inherit" stroke="#d2d2d2">
            <g strokeWidth="0.992">
              <path
                strokeLinejoin="round"
                d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                transform="rotate(-45 -450.04 415.44) rotate(180 -270.38 257.91) scale(.70584)"
              ></path>
              <path
                d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                transform="rotate(-45 -450.04 415.44) rotate(180 -270.38 257.91) scale(.70584)"
              ></path>
            </g>
            <path
              strokeLinejoin="round"
              strokeWidth="0.7"
              d="M-480.51 475.55h18.352v-18.352h-18.352z"
              transform="rotate(-45 -450.04 415.44)"
            ></path>
            <g strokeWidth="0.992">
              <path
                strokeLinejoin="round"
                d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                transform="rotate(-45 -450.04 415.44) translate(-401.91 416.92) scale(.70584)"
              ></path>
              <path
                d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                transform="rotate(-45 -450.04 415.44) translate(-401.91 416.92) scale(.70584)"
              ></path>
              <g>
                <path
                  strokeLinejoin="round"
                  d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                  transform="rotate(-45 -450.04 415.44) rotate(90 -478.835 56.955) scale(.70584)"
                ></path>
                <path
                  d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                  transform="rotate(-45 -450.04 415.44) rotate(90 -478.835 56.955) scale(.70584)"
                ></path>
              </g>
              <g>
                <path
                  strokeLinejoin="round"
                  d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                  transform="rotate(-45 -450.04 415.44) rotate(-90 -61.915 458.865) scale(.70584)"
                ></path>
                <path
                  d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                  transform="rotate(-45 -450.04 415.44) rotate(-90 -61.915 458.865) scale(.70584)"
                ></path>
              </g>
            </g>
          </g>
          <text
            x="-508.404"
            y="443.056"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="5.644"
            style={{
              lineHeight: "3.45",
              shapeInside: "url(#path3300-2-2)",
              whiteSpace: "pre",
            }}
            transform="rotate(-45 -450.04 415.44)"
          >
            <tspan x="-477.092" y="465.341">
              <tspan>№17</tspan>
            </tspan>
            <tspan x="-477.092" y="471.341">
              <tspan>
                {(bookedTablesData[17]?.bookedTableSeconds != 0) ?
                  bookedTablesData[17]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g id="tableN18"
          className={"booking-tables__table" + (
            bookedTablesData[18]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[18]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[18]?.isBooked ?
            () => bookATable(bookedTablesData[18]?.tableId) :
            bookedTablesData[18]?.isBooked &&
              bookedTablesData[18]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[18]?.tableId) :
              null}>
          <g fill="inherit" stroke="#d2d2d2">
            <g strokeWidth="0.992">
              <path
                strokeLinejoin="round"
                d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                transform="rotate(-45 -428.79 364.14) rotate(180 -270.38 257.91) scale(.70584)"
              ></path>
              <path
                d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                transform="rotate(-45 -428.79 364.14) rotate(180 -270.38 257.91) scale(.70584)"
              ></path>
            </g>
            <path
              strokeLinejoin="round"
              strokeWidth="0.7"
              d="M-480.51 475.55h18.352v-18.352h-18.352z"
              transform="rotate(-45 -428.79 364.14)"
            ></path>
            <g strokeWidth="0.992">
              <path
                strokeLinejoin="round"
                d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                transform="rotate(-45 -428.79 364.14) translate(-401.91 416.92) scale(.70584)"
              ></path>
              <path
                d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                transform="rotate(-45 -428.79 364.14) translate(-401.91 416.92) scale(.70584)"
              ></path>
              <g>
                <path
                  strokeLinejoin="round"
                  d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                  transform="rotate(-45 -428.79 364.14) rotate(90 -478.835 56.955) scale(.70584)"
                ></path>
                <path
                  d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                  transform="rotate(-45 -428.79 364.14) rotate(90 -478.835 56.955) scale(.70584)"
                ></path>
              </g>
              <g>
                <path
                  strokeLinejoin="round"
                  d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
                  transform="rotate(-45 -428.79 364.14) rotate(-90 -61.915 458.865) scale(.70584)"
                ></path>
                <path
                  d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
                  transform="rotate(-45 -428.79 364.14) rotate(-90 -61.915 458.865) scale(.70584)"
                ></path>
              </g>
            </g>
          </g>
          <text
            x="-508.404"
            y="443.056"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="5.644"
            style={{
              lineHeight: "3.45",
              shapeInside: "url(#path3300-2-2-6)",
              whiteSpace: "pre",
            }}
            transform="rotate(-45 -428.79 364.14)"
          >
            <tspan x="-477.092" y="465.341">
              <tspan>№18</tspan>
            </tspan>
            <tspan x="-477.092" y="471.341">
              <tspan>{(bookedTablesData[18]?.bookedTableSeconds != 0) ?
                bookedTablesData[18]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g id="tableN19"
          className={"booking-tables__table" + (
            bookedTablesData[19]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[19]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[19]?.isBooked ?
            () => bookATable(bookedTablesData[19]?.tableId) :
            bookedTablesData[19]?.isBooked &&
              bookedTablesData[19]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[19]?.tableId) :
              null}>
          <path
            fill="inherit"
            stroke="#d2d2d2"
            strokeLinejoin="round"
            strokeWidth="0.7"
            d="M-588.86 292.94v16.798h25.447V292.94z"
            transform="translate(100.75 225)"
          ></path>
          <g fill="inherit" stroke="#d2d2d2" strokeWidth="0.829">
            <path
              strokeLinejoin="round"
              d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
              transform="translate(100.75 225) matrix(.84824 0 0 .83988 -492.71 245.02)"
            ></path>
            <path
              d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
              transform="translate(100.75 225) matrix(.84824 0 0 .83988 -492.71 245.02)"
            ></path>
          </g>
          <g fill="inherit" stroke="#d2d2d2">
            <path
              strokeLinejoin="round"
              strokeWidth="0.829"
              d="M-278.36 70.055v18h40v-18z"
              transform="translate(100.75 225) matrix(.84824 0 0 .83988 -356.99 255.1)"
            ></path>
            <path
              strokeWidth="0.415"
              d="M-278.36 85.055h40"
              transform="translate(100.75 225) matrix(.84824 0 0 .83988 -356.99 255.1)"
            ></path>
            <path
              strokeWidth="0.829"
              d="M-258.36 70.055v18"
              transform="translate(100.75 225) matrix(.84824 0 0 .83988 -356.99 255.1)"
            ></path>
          </g>
          <text
            x="-584.145"
            y="306.421"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="8.467"
            transform="translate(100.75 225)"
            wordSpacing="0"
            style={{
              lineHeight: "2",
              shapeInside: "url(#path1887-9-5-5-6-7)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-584.78" y="300">
              <tspan>№19</tspan>
            </tspan>
            <tspan x="-584.78" y="307">
              <tspan>
                {(bookedTablesData[19]?.bookedTableSeconds != 0) ?
                  bookedTablesData[19]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g id="tableN20"
          className={"booking-tables__table" + (
            bookedTablesData[20]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[20]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[20]?.isBooked ?
            () => bookATable(bookedTablesData[20]?.tableId) :
            bookedTablesData[20]?.isBooked &&
              bookedTablesData[20]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[20]?.tableId) :
              null}>

          <g fill="inherit" stroke="#d2d2d2" strokeWidth="0.833">
            <path
              strokeLinejoin="round"
              d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
              transform="translate(100.75 225) matrix(.8415 0 0 .83988 -459.46 245.02)"
            ></path>
            <path
              d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
              transform="translate(100.75 225) matrix(.8415 0 0 .83988 -459.46 245.02)"
            ></path>
          </g>
          <g fill="inherit" stroke="#d2d2d2">
            <path
              strokeLinejoin="round"
              strokeWidth="0.833"
              d="M-278.36 70.055v18h40v-18z"
              transform="translate(100.75 225) matrix(.8415 0 0 .83988 -324.82 255.1)"
            ></path>
            <path
              strokeWidth="0.416"
              d="M-278.36 85.055h40"
              transform="translate(100.75 225) matrix(.8415 0 0 .83988 -324.82 255.1)"
            ></path>
            <path
              strokeWidth="0.833"
              d="M-258.36 70.055v18"
              transform="translate(100.75 225) matrix(.8415 0 0 .83988 -324.82 255.1)"
            ></path>
          </g>
          <path
            fill="inherit"
            stroke="#d2d2d2"
            strokeLinejoin="round"
            strokeWidth="0.7"
            d="M-554.85 292.94v16.798h25.245V292.94z"
            transform="translate(100.75 225)"
          ></path>
          <text
            x="-541.797"
            y="305.429"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="8.467"
            style={{
              lineHeight: "2",
              shapeInside: "url(#path1887-9-5-5-6-8-4)",
              whiteSpace: "pre",
            }}
            transform="translate(100.75 225)"
          >
            <tspan x="-550.864" y="300">
              <tspan>№20</tspan>
            </tspan>
            <tspan x="-550.864" y="307">
              <tspan>
                {(bookedTablesData[20]?.bookedTableSeconds != 0) ?
                  bookedTablesData[20]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g id="tableN21"
          className={"booking-tables__table" + (
            bookedTablesData[21]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[21]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[21]?.isBooked ?
            () => bookATable(bookedTablesData[21]?.tableId) :
            bookedTablesData[21]?.isBooked &&
              bookedTablesData[21]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[21]?.tableId) :
              null}>
          <path
            fill="inherit"
            stroke="#d2d2d2"
            strokeLinejoin="round"
            strokeWidth="0.7"
            d="M-588.86 292.94v16.798h25.447V292.94z"
            transform="translate(168.48 225)"
          ></path>
          <g fill="inherit" stroke="#d2d2d2" strokeWidth="0.829">
            <path
              strokeLinejoin="round"
              d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
              transform="translate(168.48 225) matrix(.84824 0 0 .83988 -492.71 245.02)"
            ></path>
            <path
              d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
              transform="translate(168.48 225) matrix(.84824 0 0 .83988 -492.71 245.02)"
            ></path>
          </g>
          <g fill="inherit" stroke="#d2d2d2">
            <path
              strokeLinejoin="round"
              strokeWidth="0.829"
              d="M-278.36 70.055v18h40v-18z"
              transform="translate(168.48 225) matrix(.84824 0 0 .83988 -356.99 255.1)"
            ></path>
            <path
              strokeWidth="0.415"
              d="M-278.36 85.055h40"
              transform="translate(168.48 225) matrix(.84824 0 0 .83988 -356.99 255.1)"
            ></path>
            <path
              strokeWidth="0.829"
              d="M-258.36 70.055v18"
              transform="translate(168.48 225) matrix(.84824 0 0 .83988 -356.99 255.1)"
            ></path>
          </g>
          <text
            x="-584.145"
            y="306.421"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="8.467"
            transform="translate(168.48 225)"
            wordSpacing="0"
            style={{
              lineHeight: "2",
              shapeInside: "url(#path1887-9-5-5-6-7-3)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-584.78" y="300">
              <tspan>№21</tspan>
            </tspan>
            <tspan x="-584.78" y="307">
              <tspan>
                {(bookedTablesData[21]?.bookedTableSeconds != 0) ?
                  bookedTablesData[21]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g id="tableN22"
          className={"booking-tables__table" + (
            bookedTablesData[22]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[22]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[22]?.isBooked ?
            () => bookATable(bookedTablesData[22]?.tableId) :
            bookedTablesData[22]?.isBooked &&
              bookedTablesData[22]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[22]?.tableId) :
              null}>

          <g fill="inherit" stroke="#d2d2d2" strokeWidth="0.833">
            <path
              strokeLinejoin="round"
              d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
              transform="translate(168.48 225) matrix(.8415 0 0 .83988 -459.46 245.02)"
            ></path>
            <path
              d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
              transform="translate(168.48 225) matrix(.8415 0 0 .83988 -459.46 245.02)"
            ></path>
          </g>
          <g fill="inherit" stroke="#d2d2d2">
            <path
              strokeLinejoin="round"
              strokeWidth="0.833"
              d="M-278.36 70.055v18h40v-18z"
              transform="translate(168.48 225) matrix(.8415 0 0 .83988 -324.82 255.1)"
            ></path>
            <path
              strokeWidth="0.416"
              d="M-278.36 85.055h40"
              transform="translate(168.48 225) matrix(.8415 0 0 .83988 -324.82 255.1)"
            ></path>
            <path
              strokeWidth="0.833"
              d="M-258.36 70.055v18"
              transform="translate(168.48 225) matrix(.8415 0 0 .83988 -324.82 255.1)"
            ></path>
          </g>
          <path
            fill="inherit"
            stroke="#d2d2d2"
            strokeLinejoin="round"
            strokeWidth="0.7"
            d="M-554.85 292.94v16.798h25.245V292.94z"
            transform="translate(168.48 225)"
          ></path>
          <text
            x="-541.797"
            y="305.429"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="8.467"
            style={{
              lineHeight: "2",
              shapeInside: "url(#path1887-9-5-5-6-8-4-1)",
              whiteSpace: "pre",
            }}
            transform="translate(168.48 225)"
          >
            <tspan x="-550.864" y="300">
              <tspan>№22</tspan>
            </tspan>
            <tspan x="-550.864" y="307">
              <tspan>
                {(bookedTablesData[22]?.bookedTableSeconds != 0) ?
                  bookedTablesData[22]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g id="tableN23" transform="translate(-.088 .058)"
          className={"booking-tables__table" + (
            bookedTablesData[23]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[23]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[23]?.isBooked ?
            () => bookATable(bookedTablesData[23]?.tableId) :
            bookedTablesData[23]?.isBooked &&
              bookedTablesData[23]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[23]?.tableId) :
              null}>

          <g fill="inherit" stroke="#d2d2d2" strokeWidth="1.026">

            <ellipse
              cx="-332.22"
              cy="532.11"
              fill="inherit"
              stroke="#d2d2d2"
              strokeWidth="0.7"
              rx="8.287"
              ry="8.287"
            ></ellipse>
            <g fill="inherit" stroke="#d2d2d2" strokeWidth="1.026">
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M239.14 157.06v4.973a5.027 5.027 45 005.027 5.027 5 5 134.69 004.973-5.027v-4.973h-10
                  M236.64 162.17a7.52 7.52 44.845 007.54 7.5 7.48 7.48 134.84 007.46-7.5"
                transform="rotate(180 -82.85 314.595) scale(.68209)"
              ></path>
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M239.14 157.06v4.973a5.027 5.027 45 005.027 5.027 5 5 134.69 004.973-5.027v-4.973h-10
                  M236.64 162.17a7.52 7.52 44.845 007.54 7.5 7.48 7.48 134.84 007.46-7.5"
                transform="rotate(90 -300.365 65.215) scale(.68209)"
              ></path>
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M239.14 157.06v4.973a5.027 5.027 45 005.027 5.027 5 5 134.69 004.973-5.027v-4.973h-10
                  M236.64 162.17a7.52 7.52 44.845 007.54 7.5 7.48 7.48 134.84 007.46-7.5"
                transform="translate(-498.75 435.02) scale(.68209)"
              ></path>
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M239.14 157.06v4.973a5.027 5.027 45 005.027 5.027 5 5 134.69 004.973-5.027v-4.973h-10
                  M236.64 162.17a7.52 7.52 44.845 007.54 7.5 7.48 7.48 134.84 007.46-7.5"
                transform="rotate(-90 134.67 563.97) scale(.68209)"
              ></path>
            </g>
          </g>
          <text
            x="-340.15"
            y="507.055"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="5.644"
            style={{
              lineHeight: "1.05",
              shapeInside: "url(#path1135-57-8-1)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-337.982" y="531.127">
              <tspan>№23</tspan>
            </tspan>
            <tspan x="-337.982" y="537.127">
              <tspan>
                {(bookedTablesData[23]?.bookedTableSeconds != 0) ?
                  bookedTablesData[23]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g id="tableN24" transform="translate(42.246 .058)"
          className={"booking-tables__table" + (
            bookedTablesData[24]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[24]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[24]?.isBooked ?
            () => bookATable(bookedTablesData[24]?.tableId) :
            bookedTablesData[24]?.isBooked &&
              bookedTablesData[24]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[24]?.tableId) :
              null}>
          <ellipse
            cx="-332.22"
            cy="532.11"
            fill="inherit"
            stroke="#d2d2d2"
            strokeWidth="0.7"
            rx="8.287"
            ry="8.287"
          ></ellipse>
          <g fill="inherit" stroke="#d2d2d2" strokeWidth="1.026">
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M239.14 157.06v4.973a5.027 5.027 45 005.027 5.027 5 5 134.69 004.973-5.027v-4.973h-10
                  M236.64 162.17a7.52 7.52 44.845 007.54 7.5 7.48 7.48 134.84 007.46-7.5"
              transform="rotate(180 -82.85 314.595) scale(.68209)"
            ></path>
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M239.14 157.06v4.973a5.027 5.027 45 005.027 5.027 5 5 134.69 004.973-5.027v-4.973h-10
                  M236.64 162.17a7.52 7.52 44.845 007.54 7.5 7.48 7.48 134.84 007.46-7.5"
              transform="rotate(90 -300.365 65.215) scale(.68209)"
            ></path>
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M239.14 157.06v4.973a5.027 5.027 45 005.027 5.027 5 5 134.69 004.973-5.027v-4.973h-10
                  M236.64 162.17a7.52 7.52 44.845 007.54 7.5 7.48 7.48 134.84 007.46-7.5"
              transform="translate(-498.75 435.02) scale(.68209)"
            ></path>
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M239.14 157.06v4.973a5.027 5.027 45 005.027 5.027 5 5 134.69 004.973-5.027v-4.973h-10
                  M236.64 162.17a7.52 7.52 44.845 007.54 7.5 7.48 7.48 134.84 007.46-7.5"
              transform="rotate(-90 134.67 563.97) scale(.68209)"
            ></path>
          </g>
          <text
            x="-340.15"
            y="507.055"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="5.644"
            style={{
              lineHeight: "1.05",
              shapeInside: "url(#path1135-57-8-1-23)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-337.982" y="531.127">
              <tspan>№24</tspan>
            </tspan>
            <tspan x="-337.982" y="537.127">
              <tspan>
                {(bookedTablesData[24]?.bookedTableSeconds != 0) ?
                  bookedTablesData[24]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g id="tableN25" transform="rotate(-45 -358.94 461.3)"
          className={"booking-tables__table" + (
            bookedTablesData[25]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[25]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[25]?.isBooked ?
            () => bookATable(bookedTablesData[25]?.tableId) :
            bookedTablesData[25]?.isBooked &&
              bookedTablesData[25]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[25]?.tableId) :
              null}>
          <ellipse
            cx="-332.22"
            cy="532.11"
            fill="inherit"
            stroke="#d2d2d2"
            strokeWidth="0.7"
            rx="8.287"
            ry="8.287"
          ></ellipse>

          <g fill="inherit" stroke="#d2d2d2" strokeWidth="1.026">
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M239.14 157.06v4.973a5.027 5.027 45 005.027 5.027 5 5 134.69 004.973-5.027v-4.973h-10
                  M236.64 162.17a7.52 7.52 44.845 007.54 7.5 7.48 7.48 134.84 007.46-7.5"
              transform="rotate(180 -82.85 314.595) scale(.68209)"
            ></path>
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M239.14 157.06v4.973a5.027 5.027 45 005.027 5.027 5 5 134.69 004.973-5.027v-4.973h-10
                  M236.64 162.17a7.52 7.52 44.845 007.54 7.5 7.48 7.48 134.84 007.46-7.5"
              transform="rotate(90 -300.365 65.215) scale(.68209)"
            ></path>
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M239.14 157.06v4.973a5.027 5.027 45 005.027 5.027 5 5 134.69 004.973-5.027v-4.973h-10
                  M236.64 162.17a7.52 7.52 44.845 007.54 7.5 7.48 7.48 134.84 007.46-7.5"
              transform="translate(-498.75 435.02) scale(.68209)"
            ></path>
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M239.14 157.06v4.973a5.027 5.027 45 005.027 5.027 5 5 134.69 004.973-5.027v-4.973h-10
                  M236.64 162.17a7.52 7.52 44.845 007.54 7.5 7.48 7.48 134.84 007.46-7.5"
              transform="rotate(-90 134.67 563.97) scale(.68209)"
            ></path>
          </g>
          <text
            x="-340.15"
            y="507.055"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="5.644"
            style={{
              lineHeight: "1.05",
              shapeInside: "url(#path1135-57-8-1-23-8)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-337.982" y="531.127">
              <tspan>№25</tspan>
            </tspan>
            <tspan x="-337.982" y="537.127">
              <tspan>
                {(bookedTablesData[25]?.bookedTableSeconds != 0) ?
                  bookedTablesData[25]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g id="tableN26"
          className={"booking-tables__table" + (
            bookedTablesData[26]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[26]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[26]?.isBooked ?
            () => bookATable(bookedTablesData[26]?.tableId) :
            bookedTablesData[26]?.isBooked &&
              bookedTablesData[26]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[26]?.tableId) :
              null}>
          <g fill="inherit" stroke="#d2d2d2" strokeWidth="0.678">
            <path
              strokeLinejoin="round"
              d="M-271.36 244.06v13
                  M-310.36 205.06v-9a4 4 135 014-4h48v65h-48a4 4 45 01-4-4v-9h39v-39z
                  M-310.36 205.06h-5v-10a8 8 135 018-8h54v75h-54a8 8 45 01-8-8v-10h5
                  M-297.36 192.06v13M-284.36 192.06v13M-271.36 192.06v13M-258.36 205.06h-13M-271.36 218.06h13M-271.36 231.06h13M-271.36 244.06h13M-271.36 244.06h-39M-297.36 244.15v13M-284.36 244.1v13"
              transform="matrix(1.0023 0 0 1.0628 -12.145 162.29)"
            ></path>
          </g>
          <path
            fill="inherit"
            stroke="#d2d2d2"
            strokeLinejoin="round"
            strokeWidth="0.7"
            d="M-319.22 384.96v31.883h30.07V384.96z"
          ></path>


          <g fill="inherit" stroke="#d2d2d2" strokeWidth="0.835">
            <path
              strokeLinejoin="round"
              d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
              transform="matrix(0 -.86349 .8144 0 -366.91 324.48)"
            ></path>
            <path
              d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
              transform="matrix(0 -.86349 .8144 0 -366.91 324.48)"
            ></path>
          </g>
          <g fill="inherit" stroke="#d2d2d2" strokeWidth="0.835">
            <path
              strokeLinejoin="round"
              d="M-106.36 52.055h16V41.108h-3v-2.053h-10v2.053h-3z"
              transform="matrix(0 -.86349 .8144 0 -366.91 308.66)"
            ></path>
            <path
              d="M-103.36 41.055v11M-93.356 41.055v11M-103.36 43.055h10"
              transform="matrix(0 -.86349 .8144 0 -366.91 308.66)"
            ></path>
          </g>
          <text
            x="-331.225"
            y="447.055"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="10.583"
            style={{
              lineHeight: "3.2",
              shapeInside: "url(#path2269-2-0-0-6)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-314.981" y="398.544">
              <tspan>№26</tspan>
            </tspan>
            <tspan x="-314.981" y="410.544">
              <tspan>
                {(bookedTablesData[26]?.bookedTableSeconds != 0) ?
                  bookedTablesData[26]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
        <g fill="#d2d2d2" strokeWidth="0.265">
          <text
            x="-560.323"
            y="420.468"
            fontFamily="Calibri"
            fontSize="10.583"
            transform="translate(0 10.319)"
            style={{
              lineHeight: "0.95",
              shapeInside: "url(#rect10212)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-575.482" y="410.228" fontSize="7.056">
              <tspan>Служебное</tspan>
            </tspan>
            <tspan x="-576.433" y="420.282">
              <tspan fontSize="7.056">помещение</tspan>
            </tspan>
          </text>
          <text
            x="-538.356"
            y="537.055"
            fontFamily="sans-serif"
            fontSize="10.583"
            style={{ lineHeight: "1.25" }}
          ></text>
          <text
            x="-540.323"
            y="538.9"
            fontFamily="Calibri"
            fontSize="16.564"
            transform="translate(0 7.938)"
            style={{
              lineHeight: "1.25",
              shapeInside: "url(#rect10218)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-548.087" y="531.548">
              <tspan>М</tspan>
            </tspan>
          </text>
        </g>
        <g fill="none">
          <path d="M-592.86 330.06v21.5h59v-21.5z"></path>
          <path
            d="M-593.36 402.56H-593.36V403.06H-593.36z"
            opacity="0.999"
          ></path>
          <path
            d="M-593.36 402.56H-523.988V442.06H-593.36z"
            opacity="0.999"
          ></path>
          <path
            d="M-593.36 442.06H-528.36V477.06H-593.36z"
            opacity="0.999"
          ></path>
          <path
            d="M-593.36 442.06H-523.36V482.06H-593.36z"
            opacity="0.999"
          ></path>
          <path d="M-576 517.06H-506V552.06H-576z" opacity="0.999"></path>
        </g>
        <g id="tableN8"
          className={"booking-tables__table" + (
            bookedTablesData[8]?.isBooked ?
              " booking-tables__table_is-booked" : "") + (
              bookedTablesData[8]?.isBookedByClient ?
                " booking-tables__table_is-booked-by-client" :
                "")}
          onClick={!bookedTablesData[8]?.isBooked ?
            () => bookATable(bookedTablesData[8]?.tableId) :
            bookedTablesData[8]?.isBooked &&
              bookedTablesData[8]?.isBookedByClient ?
              () => unbookATable(bookedTablesData[8]?.tableId) :
              null}>
          <g fill="inherit" stroke="#d2d2d2" strokeWidth="0.7">
            <g strokeLinejoin="round">
              <path d="M-305.72 235.23v32.406h16.213V235.23zM-305.6 273.06v32.406h16.213V273.06z"></path>
              <path d="M-328.91 247.11v-34.881h62.825v116.95h-62.825v-34.881h18.24v16.415h26.346v-80.021h-26.346v16.415z"></path>
              <path d="M-310.67 244.04h-13.173v-26.674h52.692v106.69h-52.692v-26.674h13.173v13.337h26.346v-80.021h-26.346z"></path>
            </g>
            <path d="M-323.84 230.7h13.173M-310.67 217.36v13.337M-297.49 217.36v13.337M-284.32 217.36v13.337M-271.15 230.7h-13.173M-284.32 244.04h13.173M-284.32 257.37h13.173M-284.32 270.71h13.173M-284.32 284.05h13.173M-284.32 297.38h13.173M-284.32 310.72h13.173M-284.32 310.72v13.337M-297.49 310.72v13.337M-310.67 310.72v13.337M-323.84 310.72h13.173"></path>
          </g>

          <path
            fill="none"
            d="M-328.36 247.06H-308.36V294.06H-328.36z"
            opacity="0.999"
          ></path>
          <text
            x="-318.723"
            y="273.405"
            fill="#d2d2d2"
            strokeWidth="0.265"
            fontFamily="Calibri"
            fontSize="8.467"
            transform="translate(0 18.521)"
            style={{
              lineHeight: "1.25",
              shapeInside: "url(#rect10241)",
              whiteSpace: "pre",
            }}
          >
            <tspan x="-324.846" y="249.463">
              <tspan>№8</tspan>
            </tspan>
            <tspan x="-324.846" y="259.463">
              <tspan>
                {(bookedTablesData[8]?.bookedTableSeconds != 0) ?
                  bookedTablesData[8]?.bookedTableSeconds : ""}
              </tspan>
            </tspan>
          </text>
        </g>
      </g>
    </svg>
  );
}