import '../App.css'
import LeftPanel from '@/components/LeftPanel'
import Chats from '@/components/Chats'

function Messenger() {

    return (
        <>
            <div className="space-x-60">
            <LeftPanel/>
            <div className='flex space-x-5 w-full'>
            <Chats/>
            </div>
            
            </div>
        </>
    )
}

export default Messenger
