import  {Avatar, AvatarImage, AvatarFallback} from "@/components/ui/avatar.tsx";
import {
    Card,
    CardDescription,
    CardHeader,
    CardTitle,
} from "@/components/ui/card"

function Follows() {

    return (
        <>
        <div className="grid grid-cols-1 hover:bg-slate-900 p-4 rounded-lg transition duration-300 ease-in-out">
           <Card className="p-4 shadow-lg rounded-lg border border-gray-200 bg-slate-600">
            <CardHeader className="flex items-center justify-between">
              <div>
                <CardTitle className="text-lg font-bold">Jacek Jaworek</CardTitle>
                <CardDescription className="text-sm text-gray-400">Od 16:00 - 24-05-2024</CardDescription>
              </div>
            <Avatar>
                <AvatarImage src="https://github.com/shadcn.png" />
                <AvatarFallback>CN</AvatarFallback>
            </Avatar>
            </CardHeader>
           </Card>
        </div>
        </>
    )
}

export default Follows
