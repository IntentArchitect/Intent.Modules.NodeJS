import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('tpt_concrete_base_class')
export class TptConcreteBaseClass {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column({ length: 250 })
  baseAttribute: string;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}