import axios from "axios"
import { Dispatch, SetStateAction } from "react"
import { useEffect, useState } from "react"
import { DataTable } from "./components/data-table"
import { columns, UserEntity } from "./components/columns"

const UserTable = () => {
    const [data, setData]: [UserEntity[], Dispatch<SetStateAction<UserEntity[]>>] = useState<UserEntity[]>([])
    // const [loading, setLoading] = useState(true)
    // const [error, setError] = useState(null)

    useEffect(() => {
        axios
            .get("https://20.2.233.181:5001/api/User")
            // .get("http://localhost:5333/api/User")
            .then((response) => {
                setData(response.data)
                // setLoading(false)
            })
        // .catch((error) => {
        //     setError(error)
        //     setLoading(false)
        // })
    }, [])

    return (
        <>
            <DataTable columns={columns} data={data} />
        </>
    )
}
export default UserTable
