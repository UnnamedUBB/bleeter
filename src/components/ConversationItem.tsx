import React from 'react'
import  {Avatar, AvatarImage, AvatarFallback} from "@/components/ui/avatarpost";

const ConversationItem = () => {
    
    return (
        <div>
            <div className={'conversation-item p-1 dark:bg-gray-700 hover:bg-slate-700 m-1 rounded-md '}>
                <div className={'flex items-center p-2  cursor-pointer  '}>
                    <div className="w-7 h-7 m-1">
                    <Avatar >
                    <AvatarImage  src="https://github.com/shadcn.png"/>
                    <AvatarFallback>CN</AvatarFallback>
                    </Avatar>
                    </div>
                    <div className="flex-grow p-2">
                        <div className="flex justify-between text-md ">
                            <div className="text-sm font-medium">Jacek Jaworek</div>
                            <div className="text-x">16:00</div>
                        </div>
                        <div className="text-sm dark:text-gray-400  w-40 truncate">
                        Wiadomość
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default ConversationItem