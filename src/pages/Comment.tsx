import '../App.css'
import LeftPanel from '@/components/LeftPanel'
import CommentContent from '@/components/ComentContent'


function Comment() {

  return (
    <>

    <div className="space-x-64">
            <LeftPanel/>
            <CommentContent/>
            
    </div>
    </>
  )
}

export default Comment
