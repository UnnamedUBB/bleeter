import '../App.css'
import LeftPanel from '@/components/LeftPanel'
import HomeContent from '@/components/HomeContent'


function Home() {

  return (
    <>

    <div className="space-x-64">
            <LeftPanel/>
            <HomeContent/>
    </div>
    </>
  )
}

export default Home
