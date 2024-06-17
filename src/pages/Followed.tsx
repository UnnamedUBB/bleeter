import '../App.css'
import LeftPanel from '@/components/LeftPanel'
import Follows from "@/components/Follows.tsx";

function Followed() {

    return (
        <>
            <div className="space-x-60">
            <LeftPanel/>
            <Follows/>
            </div>
        </>
    )
}

export default Followed
