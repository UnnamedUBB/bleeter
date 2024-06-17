
import React from 'react'
import Conversation from './Conversation';
import Messages from './Messeages';

const Chat = () => {
    return (
        <div className="">
            <div className="flex bg-slate-900 dark:bg-gray-900 rounded-xl">               
                <div className="w-80 h-screen dark:bg-gray-800 bg-slate-00 p-2 hidden md:block">
                    <div className="h-full overflow-y-auto">
                        <div className="search-chat flex p-3 ">
                            <input className="input text-sm p-3 focus:outline-none bg-slate-800 dark:bg-gray-700  w-full rounded-l-md" type="text" placeholder="Szukaj"/>
                            <div className="bg-slate-800 dark:bg-gray-00 flex justify-center items-center pr-3 text-gray-400 rounded-r-md">
                                <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
                                </svg>
                            </div>
                        </div>
                        <div className="text-lg font-semibol p-3">Wiadomości</div>
                        <Conversation/>
                    </div>
                </div>               
                <div className="flex-grow  h-screen p-2 rounded-md">
                        <Messages/>
                </div>
            </div>
        </div>
    )
}

export default Chat
