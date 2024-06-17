import {Link} from "react-router-dom";

function LeftPanel() {

  return (
    <>
    <div className="grid grid-cols-1 gap-3 h-100 fixed top-0 left-0 p-6">
        <div className="flex space-x-5 p-5">
        <img className="w-10 h-10" src="Logo.svg" alt="Witaj" />
        <p className="mt-2 font-bold font-">Bleeter</p></div>

        <Link to="/" >
        <div className="flex space-x-5 px-5 py-5 border rounded-lg border-green-950 hover:bg-slate-900 hover:px-5 hover:py-5">
            <svg xmlns="http://www.w3.org/2000/svg"
                 className="bi bi-house-fill w-10 h-10 fill-green-600" viewBox="0 0 16 16">
                <path
                    d="M8.707 1.5a1 1 0 0 0-1.414 0L.646 8.146a.5.5 0 0 0 .708.708L8 2.207l6.646 6.647a.5.5 0 0 0 .708-.708L13 5.793V2.5a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1.293z"/>
                <path d="m8 3.293 6 6V13.5a1.5 1.5 0 0 1-1.5 1.5h-9A1.5 1.5 0 0 1 2 13.5V9.293z"/>
            </svg>
            <p className="mt-3">Home</p>
        </div>
        </Link>

        <Link to="/Profile" >
        <div className="flex space-x-5 px-5 py-5 border rounded-lg border-green-950 hover:bg-slate-900 hover:px-5 hover:py-5">
            <svg xmlns="http://www.w3.org/2000/svg"
                 className="bi bi-person-fill w-10 h-10 fill-white" viewBox="0 0 16 16">
                <path d="M3 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6"/>
            </svg>
            <p className="mt-3">Profil</p>
        </div>
        </Link>

        <Link to="/Followed" >
        <div className="flex space-x-5 px-5 py-5 border rounded-lg border-green-950 hover:bg-slate-900 hover:px-5 hover:py-5">
            <svg xmlns="http://www.w3.org/2000/svg"
                 className="bi bi-heart-fill w-9 h-9 fill-red-600" viewBox="0 0 16 16">
                <path fill-rule="evenodd" d="M8 1.314C12.438-3.248 23.534 4.735 8 15-7.534 4.736 3.562-3.248 8 1.314"/>
            </svg>
            <p className="mt-1">Polubione</p></div>
        </Link>


        <Link to="/messenger" >
        <div className="flex space-x-5 px-5 py-5 border rounded-lg border-green-950 hover:bg-slate-900 hover:px-5 hover:py-5">
            <svg xmlns="http://www.w3.org/2000/svg"
                 className="bi bi-chat-square-fill w-9 h-9 fill-blue-600" viewBox="0 0 16 16">
                <path
                    d="M2 0a2 2 0 0 0-2 2v8a2 2 0 0 0 2 2h2.5a1 1 0 0 1 .8.4l1.9 2.533a1 1 0 0 0 1.6 0l1.9-2.533a1 1 0 0 1 .8-.4H14a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2z"/>
            </svg>
            <p className="mt-1">Wiadomości</p>
        </div>
            
        </Link>

        <Link to="/loginpage" >
        <div className="flex space-x-5 px-5 py-5 rounded-lg hover:bg-slate-900 hover:px-5 hover:py-5 border border-red-900">
        <svg xmlns="http://www.w3.org/2000/svg" className="bi bi-box-arrow-right  w-9 h-9 fill-red-700" viewBox="0 0 16 16">
  <path fill-rule="evenodd" d="M10 12.5a.5.5 0 0 1-.5.5h-8a.5.5 0 0 1-.5-.5v-9a.5.5 0 0 1 .5-.5h8a.5.5 0 0 1 .5.5v2a.5.5 0 0 0 1 0v-2A1.5 1.5 0 0 0 9.5 2h-8A1.5 1.5 0 0 0 0 3.5v9A1.5 1.5 0 0 0 1.5 14h8a1.5 1.5 0 0 0 1.5-1.5v-2a.5.5 0 0 0-1 0z"/>
  <path fill-rule="evenodd" d="M15.854 8.354a.5.5 0 0 0 0-.708l-3-3a.5.5 0 0 0-.708.708L14.293 7.5H5.5a.5.5 0 0 0 0 1h8.793l-2.147 2.146a.5.5 0 0 0 .708.708z"/>
</svg>
            <p className="mt-1">Wyloguj</p>
        </div>
            
        </Link>

        

    </div>
    </>
  )
}

export default LeftPanel
