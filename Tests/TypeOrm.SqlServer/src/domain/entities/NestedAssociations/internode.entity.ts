import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('internode')
export class Internode {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  internodeAttribute: string;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}