import '../App.css'
import LeftPanel from '@/components/LeftPanel'
import ProfileInfo from '@/components/ProfileInfo'

function Profile() {

  return (
    <>
    <div className="space-x-20">
            <LeftPanel/>
            <ProfileInfo/>
    </div>
    </>
  )
}

export default Profile
